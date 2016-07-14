using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FPT.Component.ExcelPlus;
using System.IO;

namespace UKPI.BlendedReport
{
    public class ExportBlend : ExporterBase<ExportError>
    {
        const int MAX_BACKUP = 99;
        const int MAX_ROW_PER_SHEET = 65536;
        const int MAX_COLUMN_PER_SHEET = 256;
        #region Properies
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }

        protected List<Dictionary<string, string>> columnNames;
        protected ExportBlendConfig config;
        protected Dictionary<KeyHierarchy<string>, int> rowIndexes;

        protected NPOI.HSSF.UserModel.HSSFWorkbook workbook;
        protected List<string> tableNames = null;
        protected string latestPeriod;
        #endregion Properies

        #region Implement abstract methods
        /// <summary>
        /// Transform source table to excel template without heading
        /// </summary>
        /// <returns></returns>
        protected override IResult<ExportError> TransformTable()
        {
            tableNames = new List<string>();
            foreach (string key in currentTables.Keys)
            {
                tableNames.Add(key);
            }
            foreach (string key in tableNames)
            {
                currentTables[key] = TransformTable(currentTables[key]);
            }

            return new ResultBase<ExportError>();
        }

        /// <summary>
        /// Add heading part to datatable
        /// </summary>
        /// <returns></returns>
        protected override IResult<ExportError> CreateHeading()
        {
            foreach (string key in tableNames)
            {
                currentTables[key] = CreateHeading(currentTables[key]);
            }
            return new ResultBase<ExportError>();
        }

        /// <summary>
        /// Render style for excel workbook
        /// </summary>
        /// <returns></returns>
        protected override IResult<ExportError> RenderStyle()
        {
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                NPOI.SS.UserModel.Sheet sheet = workbook.GetSheetAt(i);
                MergeCells(sheet);
                FormatCells(sheet);
            }
            return new ResultBase<ExportError>();
        }

        /// <summary>
        /// Write datatable to excel workbook
        /// </summary>
        /// <returns></returns>
        protected override IResult<ExportError> WriteToWorkBook()
        {
            workbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
            IResult<ExportError> result = new ResultBase<ExportError>();
            
            foreach (string key in tableNames)
            {
                if (currentTables[key].Rows.Count > MAX_ROW_PER_SHEET || currentTables[key].Columns.Count > MAX_COLUMN_PER_SHEET)
                {
                    result.Errors.Add(new ExportError(ExportErrorType.Unknown, "Not supported"));
                }
                else
                {
                    NPOI.SS.UserModel.Sheet sheet = workbook.CreateSheet(currentTables[key].TableName);
                    WriteToSheet(sheet, currentTables[key]);
                }
            }
            return result;
        }

        /// <summary>
        /// Save and close workbook
        /// </summary>
        /// <returns></returns>
        protected override IResult<ExportError> SaveAndClose()
        {
            IResult<ExportError> result = new ResultBase<ExportError>();
            try
            {
                using (FileStream fs = new FileStream(currentFile, FileMode.Create))
                {
                    workbook.Write(fs);
                }
            }
            catch (Exception)
            {
                result.Errors.Add(new ExportError(ExportErrorType.FileExisted, currentFile));
            }
            return result;
        }
        #endregion Implement abstract methods

        #region Protected methods
        protected List<string> GetKeyColumns(System.Data.DataTable table)
        {
            List<string> result = new List<string>();

            return result;
        }

        protected string TryRename(string filepath, int maxBackup)
        {
            string result = filepath;

            int i = 0;
            while (File.Exists(result) && i < maxBackup)
            {
                result = Path.ChangeExtension(filepath, "b" + i.ToString("00"));
                i++;
            }
            try
            {
                File.Move(filepath, result);
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }

        protected void WriteToSheet(NPOI.SS.UserModel.Sheet sheet, System.Data.DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                NPOI.SS.UserModel.Row row = sheet.CreateRow(i);
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    NPOI.SS.UserModel.Cell cell = row.CreateCell(j + config.StartColumn - 1);
                    string value = table.Rows[i][j].ToString();
                    if (j > config.OutletColumn && i > config.StartRow)
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            cell.SetCellValue(ParseDouble(value));
                        }
                    }
                    else
                    {
                        cell.SetCellValue(value);
                    }
                }
            }
        }

        protected double ParseDouble(string value)
        {
            try
            {
                return double.Parse(value);
            }
            catch
            {
                return 0;
            }
        }

        protected void FormatCellBox(NPOI.SS.UserModel.Sheet sheet, int startRow, int startColumn, int endRow, int endColumn, short fillColor, bool bold)
        {
            NPOI.SS.UserModel.Font font1 = workbook.CreateFont();
            font1.FontHeightInPoints = 10;
            font1.Color = NPOI.HSSF.Util.HSSFColor.AUTOMATIC.index;
            if (bold)
            {
                font1.Boldweight = 720;
            }
            NPOI.SS.UserModel.CellStyle style1 = workbook.CreateCellStyle();
            style1.SetFont(font1);
            style1.BorderBottom = NPOI.SS.UserModel.CellBorderType.THIN;
            style1.BorderTop = NPOI.SS.UserModel.CellBorderType.THIN;
            style1.BorderLeft = NPOI.SS.UserModel.CellBorderType.THIN;
            style1.BorderRight = NPOI.SS.UserModel.CellBorderType.THIN;
            style1.FillBackgroundColor = fillColor;
            style1.FillForegroundColor = fillColor;
            style1.FillPattern = NPOI.SS.UserModel.FillPatternType.SOLID_FOREGROUND;
            style1.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER;
            NPOI.SS.UserModel.DataFormat formater = workbook.CreateDataFormat();
            style1.DataFormat = formater.GetFormat(config.NumberFormat);

            NPOI.SS.UserModel.CellStyle leftStyle = workbook.CreateCellStyle();
            leftStyle.CloneStyleFrom(style1);
            leftStyle.BorderLeft = NPOI.SS.UserModel.CellBorderType.MEDIUM;

            NPOI.SS.UserModel.CellStyle rightStyle = workbook.CreateCellStyle();
            rightStyle.CloneStyleFrom(style1);
            rightStyle.BorderRight = NPOI.SS.UserModel.CellBorderType.MEDIUM;

            NPOI.SS.UserModel.CellStyle topStyle = workbook.CreateCellStyle();
            topStyle.CloneStyleFrom(style1);
            topStyle.BorderTop = NPOI.SS.UserModel.CellBorderType.MEDIUM;

            NPOI.SS.UserModel.CellStyle bottomStyle = workbook.CreateCellStyle();
            bottomStyle.CloneStyleFrom(style1);
            bottomStyle.BorderBottom = NPOI.SS.UserModel.CellBorderType.MEDIUM;

            NPOI.SS.UserModel.CellStyle topLeftStyle = workbook.CreateCellStyle();
            topLeftStyle.CloneStyleFrom(style1);
            topLeftStyle.BorderTop = NPOI.SS.UserModel.CellBorderType.MEDIUM;
            topLeftStyle.BorderLeft = NPOI.SS.UserModel.CellBorderType.MEDIUM;

            NPOI.SS.UserModel.CellStyle bottomLeftStyle = workbook.CreateCellStyle();
            bottomLeftStyle.CloneStyleFrom(style1);
            bottomLeftStyle.BorderBottom = NPOI.SS.UserModel.CellBorderType.MEDIUM;
            bottomLeftStyle.BorderLeft = NPOI.SS.UserModel.CellBorderType.MEDIUM;

            NPOI.SS.UserModel.CellStyle topRightStyle = workbook.CreateCellStyle();
            topRightStyle.CloneStyleFrom(style1);
            topRightStyle.BorderTop = NPOI.SS.UserModel.CellBorderType.MEDIUM;
            topRightStyle.BorderRight = NPOI.SS.UserModel.CellBorderType.MEDIUM;

            NPOI.SS.UserModel.CellStyle bottomRightStyle = workbook.CreateCellStyle();
            bottomRightStyle.CloneStyleFrom(style1);
            bottomRightStyle.BorderBottom = NPOI.SS.UserModel.CellBorderType.MEDIUM;
            bottomRightStyle.BorderRight = NPOI.SS.UserModel.CellBorderType.MEDIUM;

            for (int j = startRow; j <= endRow; j++)
            {
                for (int i = startColumn; i <= endColumn; i++)
                {
                    if (i == startColumn && j == startRow)
                    {
                        sheet.GetRow(j).GetCell(i).CellStyle = topLeftStyle;
                    }
                    else if (i == startColumn && j == endRow)
                    {
                        sheet.GetRow(j).GetCell(i).CellStyle = bottomLeftStyle;
                    }
                    else if (i == endColumn && j == startRow)
                    {
                        sheet.GetRow(j).GetCell(i).CellStyle = topRightStyle;
                    }
                    else if (i == endColumn && j == endRow)
                    {
                        sheet.GetRow(j).GetCell(i).CellStyle = bottomRightStyle;
                    }
                    else if (j == startRow)
                    {
                        sheet.GetRow(j).GetCell(i).CellStyle = topStyle;

                    }
                    else if (j == endRow)
                    {
                        sheet.GetRow(j).GetCell(i).CellStyle = bottomStyle;
                    }
                    else if (i == startColumn)
                    {
                        sheet.GetRow(j).GetCell(i).CellStyle = leftStyle;
                    }
                    else if (j == endRow)
                    {
                        sheet.GetRow(j).GetCell(i).CellStyle = rightStyle;
                    }
                    else
                    {
                        sheet.GetRow(j).GetCell(i).CellStyle = style1;
                    }
                }
            }
        }

        protected void FormatCells(NPOI.SS.UserModel.Sheet sheet)
        {
            NPOI.SS.UserModel.Font title = workbook.CreateFont();
            title.Color = NPOI.HSSF.Util.HSSFColor.BLUE.index;
            title.FontHeightInPoints = 18;
            title.Boldweight = 720;
            NPOI.SS.UserModel.CellStyle style1 = workbook.CreateCellStyle();
            style1.SetFont(title);
            sheet.GetRow(config.Title.Row - 1).GetCell(config.Title.Column - 1).CellStyle = style1;
            sheet.GetRow(config.Title.Row - 1).HeightInPoints = 24;

            NPOI.SS.UserModel.Font subTitleBold = workbook.CreateFont();
            subTitleBold.FontHeightInPoints = 10;
            subTitleBold.Boldweight = 720;
            subTitleBold.Color = NPOI.HSSF.Util.HSSFColor.BLUE.index;
            NPOI.SS.UserModel.CellStyle style2 = workbook.CreateCellStyle();
            style2.SetFont(subTitleBold);
            sheet.GetRow(config.RPeriod.Row - 1).GetCell(config.RPeriod.Column - 1).CellStyle = style2;
            sheet.GetRow(config.SubTitle.Row - 1).GetCell(config.SubTitle.Column - 1).CellStyle = style2;

            NPOI.SS.UserModel.Font font1 = workbook.CreateFont();
            font1.FontHeightInPoints = 10;
            font1.Color = NPOI.HSSF.Util.HSSFColor.BLUE.index;
            NPOI.SS.UserModel.CellStyle style3 = workbook.CreateCellStyle();
            style3.SetFont(font1);
            sheet.GetRow(config.Update.Row - 1).GetCell(config.Update.Column - 1).CellStyle = style3;
            sheet.GetRow(config.RPLabel.Row - 1).GetCell(config.RPLabel.Column - 1).CellStyle = style3;

            int startCol = config.StartColumn - 1;
            int endCol = config.OutletColumn - 1;
            short fill = NPOI.HSSF.Util.HSSFColor.LIGHT_GREEN.index;
            short head = NPOI.HSSF.Util.HSSFColor.SEA_GREEN.index;
            FormatCellBox(sheet, config.StartRow - 1, startCol, config.StartRow, endCol, head, true);
            FormatCellBox(sheet, config.StartRow + 1, startCol, sheet.LastRowNum, endCol, fill, false);

            startCol = endCol + 1;
            endCol += config.ToCount;
            FormatCellBox(sheet, config.StartRow - 1, startCol, config.StartRow, endCol, head, true);
            FormatCellBox(sheet, config.StartRow + 1, startCol, sheet.LastRowNum, endCol, fill, false);

            startCol = endCol + 1;
            endCol += config.PcCount;
            head = NPOI.HSSF.Util.HSSFColor.LIGHT_ORANGE.index;
            fill = NPOI.HSSF.Util.HSSFColor.TAN.index;
            FormatCellBox(sheet, config.StartRow - 1, startCol, config.StartRow, endCol, head, true);
            FormatCellBox(sheet, config.StartRow + 1, startCol, sheet.LastRowNum, endCol, fill, false);

            startCol = endCol + 1;
            endCol += config.LppcCount;
            FormatCellBox(sheet, config.StartRow - 1, startCol, config.StartRow, endCol, head, true);
            FormatCellBox(sheet, config.StartRow + 1, startCol, sheet.LastRowNum, endCol, fill, false);

            startCol = endCol + 1;
            endCol += config.VppCount;
            FormatCellBox(sheet, config.StartRow - 1, startCol, config.StartRow, endCol, head, true);
            FormatCellBox(sheet, config.StartRow + 1, startCol, sheet.LastRowNum, endCol, fill, false);

            startCol = endCol + 1;
            endCol += config.PsCount;
            head = NPOI.HSSF.Util.HSSFColor.LIGHT_BLUE.index;
            fill = NPOI.HSSF.Util.HSSFColor.SKY_BLUE.index;
            FormatCellBox(sheet, config.StartRow - 1, startCol, config.StartRow, endCol, head, true);
            FormatCellBox(sheet, config.StartRow + 1, startCol, sheet.LastRowNum, endCol, fill, false);

            startCol = endCol + 1;
            endCol += config.OsaCount;
            fill = NPOI.HSSF.Util.HSSFColor.LEMON_CHIFFON.index;
            head = NPOI.HSSF.Util.HSSFColor.LIGHT_TURQUOISE.index;
            FormatCellBox(sheet, config.StartRow - 1, startCol, config.StartRow, endCol, head, true);
            FormatCellBox(sheet, config.StartRow + 1, startCol, sheet.LastRowNum, endCol, fill, false);

            startCol = endCol + 1;
            endCol += config.NpdCount;
            fill = NPOI.HSSF.Util.HSSFColor.PALE_BLUE.index;
            head = NPOI.HSSF.Util.HSSFColor.ROYAL_BLUE.index;
            FormatCellBox(sheet, config.StartRow - 1, startCol, config.StartRow, endCol, head, true);
            FormatCellBox(sheet, config.StartRow + 1, startCol, sheet.LastRowNum, endCol, fill, false);

            startCol = endCol + 1;
            endCol += config.ShelfStdCount;
            fill = NPOI.HSSF.Util.HSSFColor.LIGHT_CORNFLOWER_BLUE.index;
            head = NPOI.HSSF.Util.HSSFColor.LIME.index;
            FormatCellBox(sheet, config.StartRow - 1, startCol, config.StartRow, endCol, head, true);
            FormatCellBox(sheet, config.StartRow + 1, startCol, sheet.LastRowNum, endCol, fill, false);

            startCol = endCol + 1;
            endCol += config.PromotionCount;
            fill = NPOI.HSSF.Util.HSSFColor.PALE_BLUE.index;
            head = NPOI.HSSF.Util.HSSFColor.ROYAL_BLUE.index;
            FormatCellBox(sheet, config.StartRow - 1, startCol, config.StartRow, endCol, head, true);
            FormatCellBox(sheet, config.StartRow + 1, startCol, sheet.LastRowNum, endCol, fill, false);
        }

        protected void MergeCells(NPOI.SS.UserModel.Sheet sheet)
        {
            List<NPOI.SS.Util.CellRangeAddress> regions = GetMergeRegions();
            foreach (NPOI.SS.Util.CellRangeAddress addr in regions)
            {
                sheet.AddMergedRegion(addr);
            }
        }

        protected List<NPOI.SS.Util.CellRangeAddress> GetMergeRegions()
        {
            List<NPOI.SS.Util.CellRangeAddress> result = new List<NPOI.SS.Util.CellRangeAddress>();
            int curCol = config.StartColumn - 1;
            NPOI.SS.Util.CellRangeAddress addr = new NPOI.SS.Util.CellRangeAddress(config.StartRow - 1, config.StartRow, curCol, curCol);
            result.Add(addr);

            curCol++;
            addr = new NPOI.SS.Util.CellRangeAddress(config.StartRow - 1, config.StartRow, curCol, curCol);
            result.Add(addr);

            curCol++;
            addr = new NPOI.SS.Util.CellRangeAddress(config.StartRow - 1, config.StartRow, curCol, curCol);
            result.Add(addr);

            curCol++;
            addr = new NPOI.SS.Util.CellRangeAddress(config.StartRow - 1, config.StartRow, curCol, curCol);
            result.Add(addr);

            curCol++;
            addr = new NPOI.SS.Util.CellRangeAddress(config.StartRow - 1, config.StartRow, curCol, curCol);
            result.Add(addr);

            curCol = config.OutletColumn;
            addr = new NPOI.SS.Util.CellRangeAddress(config.StartRow - 1, config.StartRow - 1, curCol, curCol + config.ToCount - 1);
            result.Add(addr);

            curCol += config.ToCount;
            addr = new NPOI.SS.Util.CellRangeAddress(config.StartRow - 1, config.StartRow - 1, curCol, curCol + config.PcCount - 1);
            result.Add(addr);

            curCol += config.PcCount;
            addr = new NPOI.SS.Util.CellRangeAddress(config.StartRow - 1, config.StartRow - 1, curCol, curCol + config.LppcCount - 1);
            result.Add(addr);

            curCol += config.LppcCount;
            addr = new NPOI.SS.Util.CellRangeAddress(config.StartRow - 1, config.StartRow - 1, curCol, curCol + config.VppCount - 1);
            result.Add(addr);

            curCol += config.VppCount;
            addr = new NPOI.SS.Util.CellRangeAddress(config.StartRow - 1, config.StartRow - 1, curCol, curCol + config.PsCount - 1);
            result.Add(addr);

            curCol += config.PsCount;
            addr = new NPOI.SS.Util.CellRangeAddress(config.StartRow - 1, config.StartRow - 1, curCol, curCol + config.OsaCount - 1);
            result.Add(addr);

            curCol += config.OsaCount;
            addr = new NPOI.SS.Util.CellRangeAddress(config.StartRow - 1, config.StartRow - 1, curCol, curCol + config.NpdCount - 1);
            result.Add(addr);

            curCol += config.NpdCount;
            addr = new NPOI.SS.Util.CellRangeAddress(config.StartRow - 1, config.StartRow - 1, curCol, curCol + config.ShelfStdCount - 1);
            result.Add(addr);

            curCol += config.ShelfStdCount;
            addr = new NPOI.SS.Util.CellRangeAddress(config.StartRow - 1, config.StartRow - 1, curCol, curCol + config.PromotionCount - 1);
            result.Add(addr);

            curCol += config.PromotionCount - 1;
            addr = new NPOI.SS.Util.CellRangeAddress(config.Title.Row - 1, config.Title.Row - 1, config.Title.Column - 1, curCol);
            result.Add(addr);
            addr = new NPOI.SS.Util.CellRangeAddress(config.SubTitle.Row - 1, config.SubTitle.Row - 1, config.SubTitle.Column - 1, curCol);
            result.Add(addr);

            return result;
        }

        protected System.Data.DataTable TransformTable(System.Data.DataTable table)
        {
            System.Data.DataTable result = GenerateTable();
            rowIndexes = new Dictionary<KeyHierarchy<string>, int>(new KeyHierarchyComparer<string>());
            int rowCount = rowIndexes.Count;
            List<string> columns = GetAllColumnNames(result);
            foreach (System.Data.DataRow row in table.Rows)
            {
                int month = 0;
                int year = 0;
                string dt = string.Empty;
                string region = string.Empty;
                string sup = string.Empty;
                string olID = string.Empty;
                string ol = string.Empty;
                if (row[Constant.DB_SP_EXP_MONTH] != null)
                    month = ParseInt(row[Constant.DB_SP_EXP_MONTH].ToString());
                if (row[Constant.DB_SP_EXP_YEAR] != null)
                    year = ParseInt(row[Constant.DB_SP_EXP_YEAR].ToString());
                if (row[Constant.DB_SP_EXP_DISTRIBUTOR] != null)
                    dt = row[Constant.DB_SP_EXP_DISTRIBUTOR].ToString();
                if (row[Constant.DB_SP_EXP_REGION] != null)
                    region = row[Constant.DB_SP_EXP_REGION].ToString();
                if (row[Constant.DB_SP_EXP_SUPERVISOR] != null)
                    sup = row[Constant.DB_SP_EXP_SUPERVISOR].ToString();
                if (row[Constant.DB_SP_EXP_OUTLET_ID] != null)
                    olID = row[Constant.DB_SP_EXP_OUTLET_ID].ToString();
                if (row[Constant.DB_SP_EXP_OUTLET_NAME] != null)
                    ol = row[Constant.DB_SP_EXP_OUTLET_NAME].ToString();

                KeyHierarchy<string> key = new KeyHierarchy<string>(new string[] { region, dt, olID });

                if (month > 0 && year > 0 && !string.IsNullOrEmpty(dt) && !string.IsNullOrEmpty(olID))
                {

                    if (!rowIndexes.ContainsKey(key))
                    {
                        System.Data.DataRow tmp = result.NewRow();
                        tmp[Constant.DB_SP_EXP_REGION] = region;
                        tmp[Constant.DB_SP_EXP_DISTRIBUTOR] = dt;
                        tmp[Constant.DB_SP_EXP_SUPERVISOR] = sup;
                        tmp[Constant.DB_SP_EXP_OUTLET_NAME] = ol;
                        tmp[Constant.DB_SP_EXP_OUTLET_ID] = olID;
                        result.Rows.Add(tmp);
                        rowIndexes.Add(key, rowCount);
                        rowCount++;
                    }
                    int r = rowIndexes[key];
                    UpdateRow(columns, row, result.Rows[r], year, month);
                }
            }

            return result;
        }


        protected System.Data.DataTable CreateHeading(System.Data.DataTable table)
        {
            // Insert column for header
            for (int i = columnNames.Count - 1; i >= 0; i--)
            {
                System.Data.DataRow r = table.NewRow();
                foreach (KeyValuePair<string, string> kvp in columnNames[i])
                {
                    r[kvp.Key] = kvp.Value;
                }
                table.Rows.InsertAt(r, 0);
            }

            for (int i = 0; i < config.StartRow - 1; i++)
            {
                System.Data.DataRow r = table.NewRow();
                table.Rows.InsertAt(r, 0);
            }

            // Update title
            int row = config.Title.Row - 1;
            int column = config.Title.Column - config.StartColumn;
            if (row >= 0 && column >= 0)
            {
                table.Rows[row][column] = config.Title.Value;
            }

            row = config.Update.Row - 1;
            column = config.Update.Column - config.StartColumn;
            if (row >= 0 && column >= 0)
            {
                table.Rows[row][column] = config.Update.Value;
            }

            row = config.LatestData.Row - 1;
            column = config.LatestData.Column - config.StartColumn;
            if (row >= 0 && column >= 0)
            {
                table.Rows[row][column] = latestPeriod;
            }

            row = config.RPLabel.Row - 1;
            column = config.RPLabel.Column - config.StartColumn;
            if (row >= 0 && column >= 0)
            {
                table.Rows[row][column] = config.RPLabel.Value;
            }

            row = config.RPeriod.Row - 1;
            column = config.RPeriod.Column - config.StartColumn;
            DateTime rp = new DateTime(CurrentYear, CurrentMonth, 1);
            if (row >= 0 && column >= 0)
            {
                table.Rows[row][column] = rp.ToString(config.RPMonthFormat);
            }

            row = config.SubTitle.Row - 1;
            column = config.SubTitle.Column - config.StartColumn;
            if (row >= 0 && column >= 0)
            {
                table.Rows[row][column] = config.SubTitle.Value;
            }

            return table;
        }

        protected void UpdateRow(List<string> sinkColumns, System.Data.DataRow source, System.Data.DataRow sink, int year, int month)
        {
            string key = GetMonthKey(year, month);
            if (sinkColumns.Contains(Constant.DB_SP_EXP_LPPC + key))
                sink[Constant.DB_SP_EXP_LPPC + key] = source[Constant.DB_SP_EXP_LPPC];

            if (sinkColumns.Contains(Constant.DB_SP_EXP_NPD + key))
                sink[Constant.DB_SP_EXP_NPD + key] = source[Constant.DB_SP_EXP_NPD];

            if (sinkColumns.Contains(Constant.DB_SP_EXP_OSA + key))
                sink[Constant.DB_SP_EXP_OSA + key] = source[Constant.DB_SP_EXP_OSA];

            if (sinkColumns.Contains(Constant.DB_SP_EXP_PC + key))
                sink[Constant.DB_SP_EXP_PC + key] = source[Constant.DB_SP_EXP_PC];

            if (sinkColumns.Contains(Constant.DB_SP_EXP_PROMOTION + key))
                sink[Constant.DB_SP_EXP_PROMOTION + key] = source[Constant.DB_SP_EXP_PROMOTION];

            if (sinkColumns.Contains(Constant.DB_SP_EXP_PS + key))
                sink[Constant.DB_SP_EXP_PS + key] = source[Constant.DB_SP_EXP_PS];

            if (sinkColumns.Contains(Constant.DB_SP_EXP_SHELF_STANDARD + key))
                sink[Constant.DB_SP_EXP_SHELF_STANDARD + key] = source[Constant.DB_SP_EXP_SHELF_STANDARD];

            if (sinkColumns.Contains(Constant.DB_SP_EXP_TO_VALUE + key))
                sink[Constant.DB_SP_EXP_TO_VALUE + key] = source[Constant.DB_SP_EXP_TO_VALUE];

            if (sinkColumns.Contains(Constant.DB_SP_EXP_VPP + key))
                sink[Constant.DB_SP_EXP_VPP + key] = source[Constant.DB_SP_EXP_VPP];
        }

        protected int ParseInt(string value)
        {
            try
            {
                return int.Parse(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the column name for "count" month last to current month
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        protected Dictionary<string, string> GetMonthColumnNames(int count)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (CurrentYear > 0 && CurrentMonth > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    int month = CurrentMonth - count + 1 + i;
                    int year = CurrentYear;
                    if (month <= 0)
                    {
                        year--;
                        month += Constant.MONTH_PER_YEAR;
                    }
                    DateTime m = new DateTime(year, month, 1);

                    result.Add(GetMonthKey(year, month), m.ToString(config.MonthFormat));
                }
            }
            return result;
        }

        protected string GetMonthKey(int year, int month)
        {
            KeyHierarchy<int> key = new KeyHierarchy<int>(new int[] { year, month });
            return key.ToString();
        }

        protected List<string> GetAllColumnNames(System.Data.DataTable table)
        {
            List<string> result = new List<string>();
            foreach (System.Data.DataColumn item in table.Columns)
            {
                result.Add(item.ColumnName);
            }
            return result;
        }

        protected System.Data.DataTable GenerateTable()
        {
            columnNames = new List<Dictionary<string, string>>();
            Dictionary<string, string> first = new Dictionary<string, string>();
            Dictionary<string, string> second = new Dictionary<string, string>();
            System.Data.DataTable table = new System.Data.DataTable();
            table.TableName = config.SheetName;

            table.Columns.Add(Constant.DB_SP_EXP_REGION);
            table.Columns.Add(Constant.DB_SP_EXP_DISTRIBUTOR);
            table.Columns.Add(Constant.DB_SP_EXP_SUPERVISOR);
            table.Columns.Add(Constant.DB_SP_EXP_OUTLET_ID);
            table.Columns.Add(Constant.DB_SP_EXP_OUTLET_NAME);

            first.Add(Constant.DB_SP_EXP_REGION, config.RegionText);
            first.Add(Constant.DB_SP_EXP_DISTRIBUTOR, config.DistributorText);
            first.Add(Constant.DB_SP_EXP_SUPERVISOR, config.SupText);
            first.Add(Constant.DB_SP_EXP_OUTLET_ID, config.OutletIDText);
            first.Add(Constant.DB_SP_EXP_OUTLET_NAME, config.OutletText);

            second.Add(Constant.DB_SP_EXP_REGION, config.RegionText);
            second.Add(Constant.DB_SP_EXP_DISTRIBUTOR, config.DistributorText);
            second.Add(Constant.DB_SP_EXP_SUPERVISOR, config.SupText);
            second.Add(Constant.DB_SP_EXP_OUTLET_ID, config.OutletIDText);
            second.Add(Constant.DB_SP_EXP_OUTLET_NAME, config.OutletText);

            int[] month = new int[] { CurrentYear, CurrentMonth };

            Dictionary<string, string> colNames = GetMonthColumnNames(config.ToCount);
            foreach (string item in colNames.Keys)
            {
                string name = Constant.DB_SP_EXP_TO_VALUE + item;
                table.Columns.Add(name);
                first.Add(name, config.ToText);
                second.Add(name, colNames[item]);
            }

            colNames = GetMonthColumnNames(config.PcCount);
            foreach (string item in colNames.Keys)
            {
                string name = Constant.DB_SP_EXP_PC + item;
                table.Columns.Add(name);
                first.Add(name, config.PcText);
                second.Add(name, colNames[item]);
            }

            colNames = GetMonthColumnNames(config.LppcCount);
            foreach (string item in colNames.Keys)
            {
                string name = Constant.DB_SP_EXP_LPPC + item;
                table.Columns.Add(name);
                first.Add(name, config.LppcText);
                second.Add(name, colNames[item]);
            }

            colNames = GetMonthColumnNames(config.VppCount);
            foreach (string item in colNames.Keys)
            {
                string name = Constant.DB_SP_EXP_VPP + item;
                table.Columns.Add(name);
                first.Add(name, config.VppText);
                second.Add(name, colNames[item]);
            }

            colNames = GetMonthColumnNames(config.PsCount);
            foreach (string item in colNames.Keys)
            {
                string name = Constant.DB_SP_EXP_PS + item;
                table.Columns.Add(name);
                first.Add(name, config.PsText);
                second.Add(name, colNames[item]);
            }
            colNames = GetMonthColumnNames(config.OsaCount);
            foreach (string item in colNames.Keys)
            {
                string name = Constant.DB_SP_EXP_OSA + item;
                table.Columns.Add(name);
                first.Add(name, config.OsaText);
                second.Add(name, colNames[item]);
            }
            colNames = GetMonthColumnNames(config.NpdCount);
            foreach (string item in colNames.Keys)
            {
                string name = Constant.DB_SP_EXP_NPD + item;
                table.Columns.Add(name);
                first.Add(name, config.NpdText);
                second.Add(name, colNames[item]);
            }

            colNames = GetMonthColumnNames(config.ShelfStdCount);
            foreach (string item in colNames.Keys)
            {
                string name = Constant.DB_SP_EXP_SHELF_STANDARD + item;
                table.Columns.Add(name);
                first.Add(name, config.ShelfStdText);
                second.Add(name, colNames[item]);
            }

            colNames = GetMonthColumnNames(config.PromotionCount);
            foreach (string item in colNames.Keys)
            {
                string name = Constant.DB_SP_EXP_PROMOTION + item;
                table.Columns.Add(name);
                first.Add(name, config.PromotionText);
                second.Add(name, colNames[item]);
            }

            columnNames.Add(first);
            columnNames.Add(second);
            return table;
        }

        #endregion Protected methods

        #region Public methods
        public IResult<ExportError> Export(System.Data.DataTable table, string filePath, int currentYear, int currentMonth, DateTime latestDataPeriod)
        {
            CurrentMonth = currentMonth;
            CurrentYear = currentYear;
            latestPeriod = latestDataPeriod.ToString(config.RPMonthFormat);
            IList<System.Data.DataTable> data = new List<System.Data.DataTable>();
            data.Add(table);
            return base.Export(data, filePath);
        }

        public ExportBlend(ExportBlendConfig exportConfig)
            : base()
        {
            config = exportConfig;
        }
        #endregion Public methods
    }
}
