using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FPT.Component.ExcelPlus;
using System.Data;

namespace UKPI.AuditResult
{
    public class AuditResultImporter
    {
        public const string ERROR_COLUMN_NAME = "ValidationResult";
        public const string DEFAULT_DATE_FORMAT = "yyyy-M-dd";
        public const string DB_LIST_SEPERATOR = ";";
        public const string DB_FIELD_SEPERATOR = ",";
        public const string DEFAULT_TOTAL_TEXT = "TOTAL CATEGORIES";

        public string TotalText { get; set; }
        protected string dataPeriod;
        protected AuditResultDao dao;

        public AuditResultImporter(string connectionString)
        {
            dataPeriod = string.Empty;
            DateFormat = DEFAULT_DATE_FORMAT;

            MsgInvalidPeriod = string.Empty;
            MsgInvalidDateFormat = string.Empty;
            MsgInvalidStore = string.Empty;
            MsgOutOfRange = string.Empty;
            MsgInvalidCoordinate = string.Empty;
            MsgPSDependency = string.Empty;
            MsgDuplicate = string.Empty;
            MsgUndefineStatus = string.Empty;
            MsgRequiredValue = string.Empty;
            TotalText = DEFAULT_TOTAL_TEXT;

            dao = new AuditResultDao(connectionString);
        }

        public string MsgInvalidPeriod { get; set; }
        public string MsgInvalidDateFormat { get; set; }
        public string MsgInvalidStore { get; set; }
        public string MsgOutOfRange { get; set; }
        public string MsgInvalidCoordinate { get; set; }
        public string MsgPSDependency { get; set; }
        public string MsgDuplicate { get; set; }
        public string MsgInvalidDiplaySet { get; set; }
        public string MsgInvalidCategory { get; set; }
        public string MsgUndefineStatus { get; set; }
        public string MsgRequiredValue { get; set; }

        public string DateFormat { get; set; }

        public ValidateTableResult ImportAuditResultInfo(string filePath, string xmlTemplate, string period)
        {
            CsvMapping mapping = CsvMapping.Load(xmlTemplate);
            DataTable table = CsvUtility.CsvToDataTable(filePath, mapping);
            if (table == null || table.Rows.Count == 0)
            {
                ValidateTableResult result = new ValidateTableResult();
                result.Success = true;
                return result;
            }
            return ValidateAuditInfomation(table, period, mapping.HeaderIndex);
        }

        public ValidateTableResult ImportAuditResultDetail(string filePath, string xmlTemplate, string period)
        {
            CsvMapping mapping = CsvMapping.Load(xmlTemplate);
            DataTable table = RemoveTotalRows(CsvUtility.CsvToDataTable(filePath, mapping));
            if (table == null || table.Rows.Count == 0)
            {
                ValidateTableResult result = new ValidateTableResult();
                result.Success = true;
                return result;
            }
            return ValidateAuditDetail(table, period, mapping.HeaderIndex);
        }

        protected DataTable RemoveTotalRows(DataTable table)
        {
            DataTable result = table.Clone();

            foreach (DataRow row in table.Rows)
            {
                List<string> checkCells = new List<string>();
                object obj = row[AuditResultDao.COL_CATEGORY_ID];
                if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
                {
                    checkCells.Add(obj.ToString().Trim().ToUpper());
                }
                obj = row[AuditResultDao.COL_CATEGORY_NAME];
                if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
                {
                    checkCells.Add(obj.ToString().Trim().ToUpper());
                }

                if (!checkCells.Contains(TotalText.Trim().ToUpper()))
                {
                    result.Rows.Add(row.ItemArray);
                }
            }
            return result;
        }

        protected ValidateTableResult ValidateAuditDetail(DataTable table, string period, int headerIndex)
        {
            ValidateTableResult result = new ValidateTableResult();
            table.Columns.Add(ERROR_COLUMN_NAME);
            result.Table = table;
            result.Success = true;
            CheckDataPeriod(result, period, GetAuditDateColumnIndex(result.Table));
            ValidateRangeValues(result, new List<int>(new int[] { table.Columns.IndexOf(AuditResultDao.COL_RESULT) }), new string[] { "0", "1", "" }, true);
            List<int> detailKey = new List<int>();
            detailKey.Add(table.Columns.IndexOf(AuditResultDao.COL_STORE_ID));
            detailKey.Add(table.Columns.IndexOf(AuditResultDao.COL_DISPLAY_SET));
            detailKey.Add(table.Columns.IndexOf(AuditResultDao.COL_CATEGORY_ID));
            ValidateDupplicate(result, detailKey, headerIndex + 1);
            ValidateStore(result, true, GetStoreIdIndex(result.Table), headerIndex + 1, period);
            ValidateStoreDisplaySet(result, period);
            ValidateAuditCategory(result, period);

            return result;
        }

        protected ValidateTableResult ValidateDefinedResultValue(ValidateTableResult data)
        {
            IList<AuditResultMapping> resultMapping = dao.GetAuditResultMapping();
            List<string> resultCodeCollection = new List<string>();
            foreach (AuditResultMapping item in resultMapping)
            {
                resultCodeCollection.Add(item.ResultCode.ToUpper().Trim());
            }
            if (data.Table != null && data.Table.Rows.Count > 0)
            {
                foreach (DataRow row in data.Table.Rows)
                {
                    if (row[AuditResultDao.COL_AUDIT_STATUS] == null ||
                        string.IsNullOrEmpty(row[AuditResultDao.COL_AUDIT_STATUS].ToString()))
                    {
                        data.Success = false;
                        StringBuilder text = new StringBuilder();
                        AppendMsg(text, row[ERROR_COLUMN_NAME].ToString());
                        AppendMsg(text, string.Format(MsgRequiredValue, AuditResultDao.COL_AUDIT_STATUS));
                        row[ERROR_COLUMN_NAME] = text.ToString();
                    }
                    else
                    {
                        string resultCode = row[AuditResultDao.COL_AUDIT_STATUS].ToString().Trim().ToUpper();
                        if (resultCode.Length > 2)
                            resultCode = resultCode.Trim().Substring(0, 2);
                        if (!resultCodeCollection.Contains(resultCode))
                        {
                            data.Success = false;
                            StringBuilder text = new StringBuilder();
                            AppendMsg(text, row[ERROR_COLUMN_NAME].ToString());
                            AppendMsg(text, string.Format(MsgUndefineStatus, AuditResultDao.COL_AUDIT_STATUS));
                            row[ERROR_COLUMN_NAME] = text.ToString();
                        }
                    }

                }
            }
            return data;
        }

        public void SaveAuditInfo(DataTable table, string period)
        {
            DataTable dbTable = dao.InfoTableSchema;
            InsertToDataTable(table, dbTable, DateFormat);
            List<AuditResultMapping> resultMapping = new List<AuditResultMapping>(dao.GetAuditResultMapping());

            foreach (DataRow r in dbTable.Rows)
            {
                r[AuditResultDao.COL_PERIOD] = period;
                string resultCode = r[AuditResultDao.COL_AUDIT_STATUS].ToString().Trim();
                resultCode = resultCode.Trim().Substring(0, 2).ToUpper();
                AuditResultMapping m = resultMapping.Find(x => x.ResultCode.ToUpper().Trim().Equals(resultCode));
                if (m != null)
                {
                    r[AuditResultDao.COL_AUDIT_STATUS] = m.ResultCode;
                    r[AuditResultDao.COL_AUDIT_RESULT] = m.Evaludation;
                }
            }
            dao.SaveInfo(dbTable, period);

            // Update store address
            foreach (DataRow row in table.Rows)
            {
                StringBuilder addr1 = new StringBuilder(row[AuditResultDao.COL_SHOP_NO].ToString());
                AppendText(addr1, row[AuditResultDao.COL_MARKET].ToString());
                AppendText(addr1, row[AuditResultDao.COL_STREET_NO].ToString());
                AppendText(addr1, row[AuditResultDao.COL_STREET_NAME].ToString());

                StringBuilder addr2 = new StringBuilder(row[AuditResultDao.COL_WARD].ToString());
                AppendText(addr2, row[AuditResultDao.COL_TOWN].ToString());

                StringBuilder addr3 = new StringBuilder(row[AuditResultDao.COL_CITY].ToString());
                AppendText(addr3, row[AuditResultDao.COL_PROVINCE].ToString());

                string storeId = row[AuditResultDao.COL_STORE_ID].ToString();
                dao.UpdateStoreAddress(storeId, addr1.ToString(), addr2.ToString(), addr3.ToString());
            }
        }

        public void SaveAuditDetail(DataTable table, string period)
        {
            DataTable dbTable = dao.DetailTableSchema;
            InsertToDataTable(table, dbTable, DateFormat);
            foreach (DataRow r in dbTable.Rows)
            {
                r[AuditResultDao.COL_PERIOD] = period;
            }
            dao.SaveDetail(dbTable, period);
        }

        private StringBuilder AppendText(StringBuilder source, string text)
        {
            if (source.Length != 0 && !string.IsNullOrEmpty(text))
            {
                source.Append(" - ");
            }
            source.Append(text);
            return source;
        }

        private StringBuilder AppendMsg(StringBuilder source, string msg)
        {
            if (source.Length != 0 && !string.IsNullOrEmpty(msg))
            {
                source.Append("; ");
            }
            source.Append(msg);
            return source;
        }

        protected DataTable InsertToDataTable(DataTable source, DataTable sink, string dateFormat)
        {
            foreach (DataRow row in source.Rows)
            {
                DataRow dR = sink.NewRow();
                bool insert = false;
                foreach (DataColumn col in sink.Columns)
                {
                    if (source.Columns.Contains(col.ColumnName))
                    {
                        dR[col.ColumnName] = Utility.ChangeType(row[col.ColumnName], col.DataType, dateFormat);
                        insert = true;
                    }
                }
                if (insert) sink.Rows.Add(dR);
            }
            return sink;
        }

        protected ValidateTableResult ValidateAuditInfomation(DataTable table, string period, int headerIndex)
        {
            ValidateTableResult result = new ValidateTableResult();
            table.Columns.Add(ERROR_COLUMN_NAME);
            result.Table = table;
            result.Success = true;
            CheckDataPeriod(result, period, GetAuditDateColumnIndex(result.Table));
            ValidateStore(result, false, GetStoreIdIndex(result.Table), headerIndex + 1, period);
            ValidateRangeValues(result, GetComplianceColIndex(result.Table), new string[] { "0", "1", "" }, true);
            int psIndex = table.Columns.IndexOf(AuditResultDao.COL_PS_COMP);
            List<int> dependencies = GetComplianceColIndex(result.Table);
            dependencies.Remove(psIndex);
            ValidatePSDependency(result, psIndex, dependencies);
            ValidateCoordinate(result, table.Columns.IndexOf(AuditResultDao.COL_LATITUDE));
            ValidateCoordinate(result, table.Columns.IndexOf(AuditResultDao.COL_LONGITUDE));
            ValidateDefinedResultValue(result);

            return result;
        }

        protected ValidateTableResult ValidateStoreDisplaySet(ValidateTableResult data, string period)
        {
            List<BinaryId> storeDisplaySet = new List<BinaryId>();
            foreach (DataRow row in data.Table.Rows)
            {
                storeDisplaySet.Add(new BinaryId(row[AuditResultDao.COL_STORE_ID].ToString(), row[AuditResultDao.COL_DISPLAY_SET].ToString()));
            }
            List<BinaryId> invalidCollection = dao.GetInvalidStoreDisplaySet(storeDisplaySet, period);
            if (invalidCollection.Count > 0)
            {
                data.Success = false;
                foreach (DataRow row in data.Table.Rows)
                {
                    BinaryId id = new BinaryId();
                    id.ID1 = row[AuditResultDao.COL_STORE_ID].ToString();
                    id.ID2 = row[AuditResultDao.COL_DISPLAY_SET].ToString();
                    if (invalidCollection.Contains(id))
                    {
                        StringBuilder text = new StringBuilder();
                        if (row[ERROR_COLUMN_NAME] != null)
                            AppendMsg(text, row[ERROR_COLUMN_NAME].ToString());
                        AppendMsg(text, string.Format(MsgInvalidDiplaySet, new object[] { id.ID1, id.ID2 }));
                        row[ERROR_COLUMN_NAME] = text.ToString();
                    }
                }
            }
            return data;
        }

        protected ValidateTableResult ValidateAuditCategory(ValidateTableResult data, string period)
        {
            List<BinaryId> storeCategory = new List<BinaryId>();
            foreach (DataRow row in data.Table.Rows)
            {
                storeCategory.Add(new BinaryId(row[AuditResultDao.COL_STORE_ID].ToString(), row[AuditResultDao.COL_CATEGORY_ID].ToString()));
            }
            List<BinaryId> invalidCollection = dao.GetInvalidAuditCategory(storeCategory, period);
            if (invalidCollection.Count > 0)
            {
                data.Success = false;
                foreach (DataRow row in data.Table.Rows)
                {
                    BinaryId id = new BinaryId();
                    id.ID1 = row[AuditResultDao.COL_STORE_ID].ToString();
                    id.ID2 = row[AuditResultDao.COL_CATEGORY_ID].ToString();
                    if (invalidCollection.Contains(id))
                    {
                        StringBuilder text = new StringBuilder();
                        if (row[ERROR_COLUMN_NAME] != null)
                            AppendMsg(text, row[ERROR_COLUMN_NAME].ToString());
                        AppendMsg(text, string.Format(MsgInvalidCategory, new object[] { id.ID1, id.ID2 }));
                        row[ERROR_COLUMN_NAME] = text.ToString();
                    }
                }
            }
            return data;
        }

        private List<int> GetComplianceColIndex(DataTable table)
        {
            List<int> result = new List<int>();
            result.Add(table.Columns.IndexOf(AuditResultDao.COL_PS_COMP));
            result.Add(table.Columns.IndexOf(AuditResultDao.COL_OSA_COMP));
            result.Add(table.Columns.IndexOf(AuditResultDao.COL_NPD_COMP));
            result.Add(table.Columns.IndexOf(AuditResultDao.COL_SOS_COMP));
            result.Add(table.Columns.IndexOf(AuditResultDao.COL_SHELF_STD_COMP));
            result.Add(table.Columns.IndexOf(AuditResultDao.COL_SOF_COMP));
            result.Add(table.Columns.IndexOf(AuditResultDao.COL_PROMO_COMP));
            result.Add(table.Columns.IndexOf(AuditResultDao.COL_CTA_COMP));

            return result;
        }

        private int GetAuditDateColumnIndex(DataTable table)
        {
            return table.Columns.IndexOf(AuditResultDao.COL_AUDIT_DATE);
        }

        private int GetStoreIdIndex(DataTable table)
        {
            return table.Columns.IndexOf(AuditResultDao.COL_STORE_ID);
        }

        protected ValidateTableResult CheckDataPeriod(ValidateTableResult data, string period, int pCol)
        {
            bool success = true;
            int year = int.Parse(period.Substring(0, 4));
            int month = int.Parse(period.Substring(4, 2));
            List<string> errors = new List<string>();
            for (int i = 0; i < data.Table.Rows.Count; i++)
            {
                string date = data.Table.Rows[i][pCol].ToString();
                DateTime d;
                if (DateTime.TryParseExact(date, DateFormat, null, System.Globalization.DateTimeStyles.None, out d))
                {
                    if (d.Month != month || d.Year != year)
                    {
                        errors.Add(string.Format(MsgInvalidPeriod, new object[] { data.Table.Columns[pCol].ColumnName, period }));
                        success = false;
                    }
                    else
                    {
                        errors.Add(string.Empty);
                    }
                }
                else
                {
                    success = false;
                    errors.Add(string.Format(MsgInvalidDateFormat, new object[] { data.Table.Columns[pCol].ColumnName, DateFormat }));
                }
            }
            if (!success)
            {
                for (int i = 0; i < data.Table.Rows.Count; i++)
                {
                    StringBuilder text = new StringBuilder();
                    if (data.Table.Rows[i][ERROR_COLUMN_NAME] != null && !string.IsNullOrEmpty(data.Table.Rows[i][ERROR_COLUMN_NAME].ToString()))
                        AppendMsg(text, data.Table.Rows[i][ERROR_COLUMN_NAME].ToString());
                    if (!string.IsNullOrEmpty(errors[i]))
                        AppendMsg(text, errors[i]);
                    data.Table.Rows[i][ERROR_COLUMN_NAME] = text.ToString();
                }
                data.Success = false;
            }
            return data;
        }

        protected ValidateTableResult ValidateStore(ValidateTableResult data, bool allowDuplicate, int storeIndex, int baseIndex, string period)
        {
            if (!allowDuplicate)
            {
                data = ValidateDupplicate(data, new List<int>(new int[] { storeIndex }), baseIndex);
            }
            List<int> invalidStoreIdCollection = GetInvalidAuditStore(data.Table, storeIndex, period);
            if (invalidStoreIdCollection.Count > 0)
            {
                foreach (int id in invalidStoreIdCollection)
                {
                    StringBuilder text = new StringBuilder();
                    if (data.Table.Rows[id][ERROR_COLUMN_NAME] != null)
                        AppendMsg(text, data.Table.Rows[id][ERROR_COLUMN_NAME].ToString());
                    AppendMsg(text, string.Format(MsgInvalidStore, new object[] { data.Table.Columns[storeIndex].ColumnName, period }));
                    data.Table.Rows[id][ERROR_COLUMN_NAME] = text.ToString();

                }
                data.Success = false;
            }

            return data;
        }

        protected List<int> GetInvalidAuditStore(DataTable table, int storeIndex, string period)
        {
            List<string> storeIdCollection = new List<string>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                storeIdCollection.Add(table.Rows[i][storeIndex].ToString());
            }
            List<string> invalid = dao.GetInvalidAuditStoreNoRegionChannel(storeIdCollection, period);
            List<int> result = new List<int>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string storeId = table.Rows[i][storeIndex].ToString();
                if (invalid.Contains(storeId) || storeId == string.Empty || storeId == "")
                {
                    result.Add(i);
                }
            }
            return result;
        }

        protected string BuildSqlParamStrings(List<string> items)
        {
            string tmp = string.Empty;

            foreach (string item in items)
            {
                tmp += item + DB_FIELD_SEPERATOR;
            }
            if (!string.IsNullOrEmpty(tmp))
            {
                tmp = tmp.Trim(DB_FIELD_SEPERATOR.ToCharArray());
            }

            return tmp;
        }

        protected ValidateTableResult ValidateRangeValues(ValidateTableResult data, IEnumerable<int> indexes, IEnumerable<string> values, bool trimSpace)
        {
            bool success = true;
            foreach (DataRow r in data.Table.Rows)
            {
                foreach (int id in indexes)
                {
                    string val = r[id].ToString();
                    if (trimSpace) val = val.Trim();
                    string temValues = "";
                    foreach (string str in values)
                    {
                        temValues += str == "" ? "'' ''" + "," : str + ",";
                    }
                    temValues = temValues.Substring(0, temValues.Length - 1);
                    if (!values.Contains(val))
                    {
                        string outOfRangeMsg = string.Format(MsgOutOfRange, new object[] { data.Table.Columns[id].ColumnName, temValues });
                        StringBuilder text = new StringBuilder();
                        if (r[ERROR_COLUMN_NAME] != null && !string.IsNullOrEmpty(r[ERROR_COLUMN_NAME].ToString()))
                            AppendMsg(text, r[ERROR_COLUMN_NAME].ToString());
                        AppendMsg(text, outOfRangeMsg);
                        r[ERROR_COLUMN_NAME] = text.ToString();
                        success = false;
                    }
                }

            }
            if (!success) data.Success = false;
            return data;
        }

        protected ValidateTableResult ValidateCoordinate(ValidateTableResult data, int index)
        {
            bool success = true;
            foreach (DataRow r in data.Table.Rows)
            {
                string val = r[index].ToString();
                if (!string.IsNullOrEmpty(val) && !Utility.IsNumeric(val))
                {
                    string coordinateMsg = string.Format(MsgInvalidCoordinate, new object[] { data.Table.Columns[index].ColumnName });
                    StringBuilder text = new StringBuilder();
                    if (r[ERROR_COLUMN_NAME] != null && !string.IsNullOrEmpty(r[ERROR_COLUMN_NAME].ToString()))
                        AppendMsg(text, r[ERROR_COLUMN_NAME].ToString());
                    AppendMsg(text, coordinateMsg);
                    r[ERROR_COLUMN_NAME] = text.ToString();
                    success = false;
                }
            }
            if (!success) data.Success = false;
            return data;
        }

        protected ValidateTableResult ValidatePSDependency(ValidateTableResult data, int psIndex, List<int> dependencies)
        {
            bool success = true;
            foreach (DataRow r in data.Table.Rows)
            {
                string val = r[psIndex].ToString().Trim();
                if (Utility.CheckIntValue(r[psIndex], 1))
                {
                    foreach (int d in dependencies)
                    {
                        if (Utility.CheckIntValue(r[d], 0))
                        {
                            // Illegal value at column d
                            string illegalValue = string.Format(MsgPSDependency, new object[] { data.Table.Columns[d].ColumnName });
                            StringBuilder text = new StringBuilder();
                            if (r[ERROR_COLUMN_NAME] != null && !string.IsNullOrEmpty(r[ERROR_COLUMN_NAME].ToString()))
                                AppendMsg(text, r[ERROR_COLUMN_NAME].ToString());
                            AppendMsg(text, illegalValue);
                            r[ERROR_COLUMN_NAME] = text.ToString();
                            success = false;
                        }
                    }
                }
            }
            if (!success) data.Success = false;
            return data;
        }

        protected ValidateTableResult ValidateDupplicate(ValidateTableResult data, List<int> keyIndexes, int baseIndex)
        {
            List<KeyComparer> keyTable = new List<KeyComparer>();
            for (int i = 0; i < data.Table.Rows.Count; i++)
            {
                List<string> key = new List<string>();
                for (int k = 0; k < keyIndexes.Count; k++)
                {
                    key.Add(data.Table.Rows[i][keyIndexes[k]].ToString());
                }
                keyTable.Add(new KeyComparer(i + baseIndex, key));
            }
            List<int[]> dup = Utility.GetDuplicateRows(keyTable);
            if (dup.Count > 0)
            {
                StringBuilder kCol = new StringBuilder();
                for (int i = 0; i < keyIndexes.Count; i++)
                {
                    kCol = AppendText(kCol, data.Table.Columns[keyIndexes[i]].ColumnName);
                }
                foreach (int[] item in dup)
                {
                    foreach (int i in item)
                    {
                        string dupErrMsg = string.Format(MsgDuplicate, new object[] { kCol.ToString() });
                        StringBuilder text = new StringBuilder();
                        if (data.Table.Rows[i - baseIndex][ERROR_COLUMN_NAME] != null && !string.IsNullOrEmpty(data.Table.Rows[i - baseIndex][ERROR_COLUMN_NAME].ToString()))
                            AppendMsg(text, data.Table.Rows[i - baseIndex][ERROR_COLUMN_NAME].ToString());
                        AppendMsg(text, dupErrMsg);
                        data.Table.Rows[i - baseIndex][ERROR_COLUMN_NAME] = text.ToString();
                    }
                }
                data.Success = false;
            }
            return data;
        }

        protected ValidateTableResult ValidateTable(DataTable table, CsvMapping mapping)
        {
            ValidateTableResult result = new ValidateTableResult();
            DataTable tmp = new DataTable();
            mapping.SortColumn();
            for (int i = 0; i < mapping.ColumnCollection.Count; i++)
            {
                tmp.Columns.Add(mapping.ColumnCollection[i].Name, mapping.ColumnCollection[i].ColumnType.DataType);
            }
            List<string> rowErrors = new List<string>();
            bool success = true;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                StringBuilder error = new StringBuilder();
                DataRow r = tmp.NewRow();
                foreach (var col in mapping.ColumnCollection)
                {
                    try
                    {
                        r[i] = Utility.ChangeType(table.Rows[i][col.Index], col.ColumnType.DataType, col.ColumnType.Format);
                    }
                    catch (Exception ex)
                    {
                        error.AppendLine(ex.ToString());
                        if (success) success = false;
                    }
                }
                tmp.Rows.Add(r);
                rowErrors.Add(error.ToString());
            }

            if (success)
            {
                result.Table = tmp;
            }
            else
            {
                table.Columns.Add(ERROR_COLUMN_NAME);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    table.Rows[i][ERROR_COLUMN_NAME] = rowErrors[i];
                }
                result.Table = table;
            }

            return result;
        }
    }

    public class ValidateTableResult
    {
        public DataTable Table { get; set; }
        public bool Success { get; set; }

        public ValidateTableResult()
        {
            Table = null;
            Success = false;
        }
    }
}
