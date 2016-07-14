using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using System.IO;
using Excel;


namespace UKPI.Utils
{
    public struct structExcelWrittingInfo
    {
        public string WriteName;
        public int ColumnIndex;

        public structExcelWrittingInfo(string name, int locaion)
        {
            WriteName = name;
            ColumnIndex = locaion;
        }
    }

    public class clsExcelWrittingMap
    {
        private Dictionary<string, structExcelWrittingInfo> m_mapTable = new Dictionary<string,structExcelWrittingInfo>();

        public Dictionary<string, structExcelWrittingInfo> MapTable
        {
            get { return m_mapTable; }
            //set { mapTable = value; }
        }

        /// <summary>
        /// Add new item
        /// </summary>
        /// <param name="fieldName">FieldName in DataTable</param>
        /// <param name="writeName">Name will be writen in Excel</param>
        /// <param name="writeColumnIndex">ColumnIndex to write in Excel</param>
        public void Add(string fieldName, string writeName, int writeColumnIndex)
        {
            m_mapTable.Add(fieldName, new structExcelWrittingInfo(writeName, writeColumnIndex));
        }
    }
    
    public class clsExcelHelper
    {
        private static int MAX_COL_INDEX = 255;
        private static int MAX_ROW_INDEX = 65535;

        private Excel.Application _excelApplication = null;
        private Excel.Workbooks _workBooks = null;
        private Excel._Workbook _workBook = null;
        private object _value = Missing.Value;
        private Excel.Sheets _excelSheets = null;
        private Excel._Worksheet _excelSheet = null;
        private Excel.Range _excelRange = null;

        #region Show/Hide Excel App

        public bool AppVisible
        {
            set { _excelApplication.Visible = value; }
        }

        #endregion Show/Hide Excel App

        #region OpenApp
        public void OpenExcelApp()
        {
            _excelApplication = new Excel.Application();
            _excelApplication.Visible = false;

            _workBooks = (Excel.Workbooks)_excelApplication.Workbooks;
            _workBook = (Excel._Workbook)(_workBooks.Add(_value));

            _excelSheets = (Excel.Sheets)_workBook.Worksheets;
            _excelSheet = (Excel._Worksheet)(_excelSheets.get_Item(1));
        }

        public void OpenExcelApp(bool visible)
        {
            _excelApplication = new Excel.Application();
            _excelApplication.Visible = visible;

            _workBooks = (Excel.Workbooks)_excelApplication.Workbooks;
            _workBook = (Excel._Workbook)(_workBooks.Add(_value));

            _excelSheets = (Excel.Sheets)_workBook.Worksheets;
            _excelSheet = (Excel._Worksheet)(_excelSheets.get_Item(1));
        }

        public void OpenExcelAppWithFile(string filePath, bool visible)
        {
            _excelApplication = new Excel.Application();
            _excelApplication.Visible = visible;

            _workBooks = (Excel.Workbooks)_excelApplication.Workbooks;
            _workBook = (Excel._Workbook)(_workBooks.Add(filePath));

            _excelSheets = (Excel.Sheets)_workBook.Worksheets;
            _excelSheet = (Excel._Worksheet)(_excelSheets.get_Item(1));
        }

        public void OpenExcelAppWithFile(string filePath, int sheetIndex, bool visible)
        {
            _excelApplication = new Excel.Application();
            _excelApplication.Visible = visible;

            _workBooks = (Excel.Workbooks)_excelApplication.Workbooks;
            _workBook = (Excel._Workbook)(_workBooks.Add(filePath));

            _excelSheets = (Excel.Sheets)_workBook.Worksheets;
            _excelSheet = (Excel._Worksheet)(_excelSheets.get_Item(sheetIndex));
        }

       //==========================================================================
        #region - API method -
        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(Int32 hWnd, ref IntPtr lpdwProcessId);
        #endregion

        public static void KillExcelApplication(Excel.Application excelApp)
        {
            #region - release excel instance -
            IntPtr xlsApplicationProcessID = new IntPtr(0);
            GetWindowThreadProcessId(excelApp.Hwnd, ref xlsApplicationProcessID);
            System.Diagnostics.Process.GetProcessById(Convert.ToInt32(xlsApplicationProcessID.ToString())).Kill();
            #endregion
        }
        //===========================================================================
        
        

        public void CloseExcel()
        {
           /* _excelApplication.Workbooks.Close();
            _excelApplication.Quit();

            //Release COM object
            NAR(_excelRange);
            NAR(_excelSheet);
            NAR(_excelSheets);
            NAR(_workBook);
            NAR(_workBooks);
            NAR(_excelApplication);

            GC.Collect();*/
            //===========================================================================================
            KillExcelApplication(_excelApplication);
            //===========================================================================================
            
        }
        #endregion OpenApp

        public void DisposeExcel()
        {
            //Release COM object
            NAR(_excelRange);
            NAR(_excelSheet);
            NAR(_excelSheets);
            NAR(_workBook);
            NAR(_workBooks);
            NAR(_excelApplication);

            GC.Collect();
        }

        #region EndProcess


        public void KillProcess(Process[] preExcelProcesses)
        {
            Process[] excelProcesses = Process.GetProcessesByName("EXCEL");
            foreach (Process process in excelProcesses)
            {
                if (!Contain(preExcelProcesses, process))
                {
                    process.Kill();
                }
            }
        }

        private bool Contain(Process[] processes, Process process)
        {
            foreach (Process p in processes)
            {
                if (p.Id == process.Id)
                    return true;
            }
            return false;
        }

        public static void EndProcess()
        {

            foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
            {
                proc.Kill();
            }
        }

        #endregion

        #region WriteData
        public void WriteCell(string cellAddress, object value)
        {
            _excelRange = _excelSheet.get_Range(cellAddress, _value);
            //_excelRange = _excelRange.get_Resize(10, 4);
            _excelRange.set_Value(_value, value);
            _excelRange.Cells.Font.Name = "Arial";
            _excelRange.Cells.Font.Size = 10;
        }

      
        public void WriteCell(string cellAddress, object value, bool writeAsText)
        {
            string newValue = "=\"" + value.ToString() + "\"";

            WriteCell(cellAddress, newValue);
        }

        public void WriteBlock(string startCellAddress, int rowCount, int colCount, object dataBlock)
        {
            _excelRange = _excelSheet.get_Range(startCellAddress, _value);
            _excelRange = _excelRange.get_Resize(rowCount, colCount);
            _excelRange.set_Value(_value, dataBlock);
        }

        public void WriteBlock(int rowIndex, int colIndex, int rowCount, int colCount, object dataBlock)
        {
            _excelRange = _excelSheet.get_Range(GetExcelAddress(colIndex, rowIndex), _value);
            _excelRange = _excelRange.get_Resize(rowCount, colCount);
            _excelRange.set_Value(_value, dataBlock);
        }

        public void WriteDataTable(int colIndex, int rowIndex, System.Data.DataTable dt, bool isWriteColumnHeader, string preChar)
        {
            int addedRowIndex = 0;
            
            if (isWriteColumnHeader)
            {
                int addedColIndex = 0;
                foreach (DataColumn col in dt.Columns)
                {
                    WriteCell(GetExcelAddress(colIndex + addedColIndex, rowIndex + addedRowIndex), col.ColumnName);
                    addedColIndex++;
                }

                addedRowIndex++;
            }

            foreach (DataRow row in dt.Rows)
            {
                for (int tableColIndex = 0; tableColIndex < dt.Columns.Count; tableColIndex++)
                {
                    WriteCell(GetExcelAddress(colIndex + tableColIndex, rowIndex + addedRowIndex), preChar + row[tableColIndex].ToString());
                }

                addedRowIndex++;
            }
            ////DongTC
            //_workSheet = Activate(excel);
            //SetAutoFitToColumns(_workSheet, dt);
        }

        public void WriteDataTable(int colIndex, int rowIndex, System.Data.DataTable dt, 
            bool isWriteColumnHeader, clsExcelWrittingMap writtingMap, string addedPrefix)
        {
            int addedRowIndex = 0;

            Dictionary<string, structExcelWrittingInfo> writtingMappingTable = writtingMap.MapTable;

            if (isWriteColumnHeader)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    if (writtingMappingTable.ContainsKey(col.ColumnName))
                    {
                        int addedColIndex = writtingMappingTable[col.ColumnName].ColumnIndex;
                        string fieldNameInExcel = writtingMappingTable[col.ColumnName].WriteName;

                        WriteCell(GetExcelAddress(colIndex + addedColIndex, rowIndex + addedRowIndex), fieldNameInExcel);
                    }
                }

                addedRowIndex++;
            }

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    if (writtingMappingTable.ContainsKey(col.ColumnName))
                    {
                        int addedColIndex = writtingMappingTable[col.ColumnName].ColumnIndex;
                        string writeValue = row[col.ColumnName].ToString();

                        WriteCell(GetExcelAddress(colIndex + addedColIndex, rowIndex + addedRowIndex), addedPrefix + writeValue);
                    }
                }

                addedRowIndex++;
            }
            
        }

        public void WriteHeader(int colIndex, int rowIndex, clsExcelWrittingMap writtingMap)
        {
            Dictionary<string, structExcelWrittingInfo> writtingMappingTable = writtingMap.MapTable;

            foreach (string key in writtingMappingTable.Keys)
            {
                int addedColIndex = writtingMappingTable[key].ColumnIndex;
                string fieldNameInExcel = writtingMappingTable[key].WriteName;

                WriteCell(GetExcelAddress(colIndex + addedColIndex, rowIndex), fieldNameInExcel);
            }

        }

        #endregion

        #region Read Data
        public object ReadCell(string cellAddress)
        {
            Excel.Range range = _excelSheet.get_Range(cellAddress, cellAddress);
            return range.get_Value(_value);
        }

        //DongTC
        public  object ReadCell_SheetName(string cellAddress, string sheetName)
        {
            ReadCell(cellAddress);
            SelectSheetByName(sheetName, true);
            return ReadCell(cellAddress);
        }

        public string ReadCellAsString(string cellAddress, string defaultValue)
        {
            Excel.Range range = _excelSheet.get_Range(cellAddress, cellAddress);
            object curObj = range.get_Value(_value);

            if (curObj != null)
            {
                return curObj.ToString();
            }

            return defaultValue;
        }

        public object[,] ReadRange(string fromAddress, string toAddress)
        {
            Excel.Range range = _excelSheet.get_Range(fromAddress, toAddress);
            return (object[,])range.get_Value(_value);
        }

        public System.Data.DataTable ReadDataAsDataTable(string fromAddress, string toAddress, bool firstRowIsHeader)
        {
            System.Data.DataTable result = new System.Data.DataTable();
            object[,] tableData = ReadRange(fromAddress, toAddress);

            int rowCount = tableData.GetLength(0);
            int colCount = tableData.GetLength(1);

            for (int curRow = 1; curRow <= rowCount; curRow++)
            {
                bool readRowAsData = true;

                if (curRow == 1 && firstRowIsHeader)
                {
                    for (int curCol = 1; curCol <= colCount; curCol++)
                    {
                        result.Columns.Add(GetArrayValueAsString(tableData, curRow, curCol, string.Empty));
                    }

                    readRowAsData = false;
                }

                if (readRowAsData)
                {
                    DataRow row = result.NewRow();

                    for (int curCol = 1; curCol <= colCount; curCol++)
                    {
                        row[curCol - 1] = GetArrayValueAsString(tableData, curRow, curCol, string.Empty);
                    }

                    result.Rows.Add(row);
                }
            }

            return result;
        }

        //DongTC
        public System.Data.DataTable ReadDataAdDataTableForImport(string filename, string sheetname, int startRow, int startColunm, int basedRow, bool firstRowIsHeader, string endValue)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SelectSheetByName(sheetname, true);
            dt = ReadDataAsDataTable(startRow, startColunm, basedRow, firstRowIsHeader, endValue);
            return dt;
        }
        
        public System.Data.DataTable ReadDataAsDataTable(int startRow, int startColunm, int basedRow, int basedCol, bool firstRowIsHeader, string endValue)
        {
            //Loop to get reange to read
            int endColumnIndex = -1;
            int endRowIndex = -1;

            int loopIndex = startColunm;
            
            //Loop to get endCol;
            do
            {
                if (loopIndex <= MAX_COL_INDEX)
                {
                    string curCellAddress = GetExcelAddress(loopIndex, basedRow);
                    string strCurValue = ReadCellAsString(curCellAddress, string.Empty); ;

                    //If current value = endValue --> set end column end break
                    if (strCurValue == endValue)
                    {
                        endColumnIndex = loopIndex - 1;
                        break;
                    }
                }
                else    //End of columns
                {
                    endColumnIndex = MAX_COL_INDEX;
                    break;
                }

                loopIndex++;
            }
            while (true);

            //Loop to get endRowIndex
            loopIndex = startRow;
            do
            {
                if (loopIndex <= MAX_ROW_INDEX)
                {
                    string curCellAddress = GetExcelAddress(basedCol, loopIndex);
                    string strCurValue = ReadCellAsString(curCellAddress, string.Empty);

                    //If current value = endValue --> set end column end break
                    if (strCurValue == endValue)
                    {
                        endRowIndex = loopIndex - 1;
                        break;
                    }
                }
                else
                {
                    endRowIndex = MAX_ROW_INDEX;
                    break;
                }

                loopIndex++;
            }
            while (true);

            //Calculate end address
            string beginAddress = GetExcelAddress(startColunm, startRow);
            string endAddress = GetExcelAddress(endColumnIndex, endRowIndex);

            return ReadDataAsDataTable(beginAddress, endAddress, firstRowIsHeader);
        }

        #region Read to empty Row

        /// <summary>
        /// Read until reach to an empty row (values on all columns is empty)
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="startColunm"></param>
        /// <param name="basedRow"></param>
        /// <param name="basedCol"></param>
        /// <param name="firstRowIsHeader"></param>
        /// <param name="endValue"></param>
        /// <returns></returns>
        public System.Data.DataTable ReadDataAsDataTable(int startRow, int startColunm, int basedRow, bool firstRowIsHeader, string endValue)
        {
            System.Data.DataTable result = new System.Data.DataTable();
            
            //Loop to get range to read
            int endColumnIndex = -1;

            int loopIndex = startColunm;

            //Loop to get endCol;
            do
            {
                if (loopIndex <= MAX_COL_INDEX)
                {
                    string curCellAddress = GetExcelAddress(loopIndex, basedRow);
                    string strCurValue = ReadCellAsString(curCellAddress, string.Empty); ;

                    //If current value = endValue --> set end column end break
                    if (strCurValue == endValue)
                    {
                        endColumnIndex = loopIndex - 1;
                        break;
                    }
                }
                else    //End of columns
                {
                    endColumnIndex = MAX_COL_INDEX;
                    break;
                }

                loopIndex++;
            }
            while (true);

            if (endColumnIndex >= startColunm)
            {

                //READ DATA
                bool firstRow = true;
                bool continueReading = true;

                int deadingRowIndex = startRow;

                do
                {
                    bool readRowData = true;

                    bool isNotEmptyRow = false;

                    if (firstRow)
                    {
                        firstRow = false;

                        if (firstRowIsHeader)
                        {
                            isNotEmptyRow = ReadToColumnIfNotEmpty(result,
                                clsExcelHelper.GetExcelAddress(startColunm, deadingRowIndex),
                                clsExcelHelper.GetExcelAddress(endColumnIndex, deadingRowIndex), string.Empty);

                            readRowData = false;
                        }
                        else
                        {
                            CreateColumns(result, endColumnIndex - startColunm + 1);
                        }
                    }

                    if (readRowData)
                    {
                        DataRow row = result.NewRow();

                        isNotEmptyRow = ReadToDataRowIfNotEmpty(row,
                            clsExcelHelper.GetExcelAddress(startColunm, deadingRowIndex),
                            clsExcelHelper.GetExcelAddress(endColumnIndex, deadingRowIndex), string.Empty);

                        if (isNotEmptyRow)
                        {
                            result.Rows.Add(row);
                        }
                    }

                    if (!isNotEmptyRow)
                    {
                        continueReading = false;
                    }

                    deadingRowIndex++;
                }
                while (continueReading);
            }

            return result;
        }

        private bool ReadToDataRowIfNotEmpty(DataRow row, string startAddress, string endAddress, string defaultValue)
        {
            object[,] data = ReadRange(startAddress, endAddress);

            object[] rowData = GetOneRow(data, 1);

            bool isRowNotEmpty = false;

            for(int i = 0; i< rowData.Length; i++)
            {
                if (rowData[i] != null)
                {
                    row[i] = rowData[i];

                    isRowNotEmpty = true;
                }
                else
                {
                    row[i] = defaultValue;
                }
            }

            return isRowNotEmpty;
        }

        private bool ReadToColumnIfNotEmpty(System.Data.DataTable dt, string startAddress, string endAddress, string defaultValue)
        {
            object[,] data = ReadRange(startAddress, endAddress);

            object[] rowData = GetOneRow(data, 1);

            bool isRowNotEmpty = false;

            for (int i = 0; i < rowData.Length; i++)
            {
                if (rowData[i] != null)
                {
                    dt.Columns.Add(rowData[i].ToString());

                    isRowNotEmpty = true;
                }
                else
                {
                    dt.Columns.Add(defaultValue);
                }
            }

            //Clear columns if row is empty
            if (!isRowNotEmpty)
            {
                dt.Columns.Clear();
            }

            return isRowNotEmpty;
        }

        private void CreateColumns(System.Data.DataTable dt, int colNum)
        {
            for (int i = 1; i <= colNum; i++)
            {
                dt.Columns.Add("Column_" + i.ToString());
            }
        }

        private object[] GetOneRow(object[,] data, int rowIndex)
        {
            int len = data.GetLength(rowIndex);
            
            object[] result = new object[len];

            for (int i = 1; i <= len; i++)
            {
                result[i-1] = data[rowIndex, i];
            }

            return result;
        }

        #endregion Read to empty Row

        public static System.Data.DataTable ReadDataUsingOleDB(string filePath)
        {
            return ReadDataUsingOleDB(filePath, "Sheet1");
        }
        
        public static System.Data.DataTable ReadDataUsingOleDB(string filePath, string sheetName)
        {
            if (System.IO.File.Exists(filePath) == false)
            {
                throw new Exception("Excel file " + filePath + "could not be found.");
            }

            string oleConnectionString = 
                @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                @"Data Source=" + filePath + ";" +
                @"Extended Properties=" + Convert.ToChar(34).ToString() +
                @"Excel 8.0;" + "Imex=1;" + Convert.ToChar(34).ToString();

            OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
            System.Data.DataTable dt = new System.Data.DataTable(filePath);
            
            OleDbConnection oleConn = new OleDbConnection(oleConnectionString);
            oleConn.Open();

            OleDbCommand oleCmdSelect = null;

            if (oleConn.State != ConnectionState.Open)
                throw new Exception("Connection cannot open.");

            try
            {
                oleCmdSelect = new OleDbCommand(@"SELECT * FROM [" + sheetName + "$" + "" + "]", oleConn);

                oleAdapter.SelectCommand = oleCmdSelect;
                oleAdapter.FillSchema(dt, SchemaType.Source);
                oleAdapter.Fill(dt);

                oleCmdSelect.Dispose();
                oleCmdSelect = null;

                oleAdapter.Dispose();
                oleAdapter = null;

                oleConn.Close();
                oleConn = null;

                return dt;
            }
            catch
            {
                if (oleCmdSelect != null)
                {
                    oleCmdSelect.Dispose();
                    oleCmdSelect = null;
                }

                if (oleAdapter != null)
                {
                    oleAdapter.Dispose();
                    oleAdapter = null;
                }

                if (oleConn != null)
                {
                    oleConn.Close();
                    oleConn = null;
                }

                throw;
            }
        }
        #endregion Read Data

        #region Format
        public void SetFont(string fromAddress, string toAddress, string fontName, int fontSize)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.Font.Name = fontName;
            _excelRange.Font.Size = fontSize;
        }

       
        public void SetBold(string fromAddress, string toAddress)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.Font.Bold = true;
        }

        public void SetItalic(string fromAddress, string toAddress)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.Font.Italic = true;
        }

        public void SetUndeline(string fromAddress, string toAddress)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.Font.Underline = true;
        }

        public void SetStrikeThrough(string fromAddress, string toAddress)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.Font.Strikethrough = true;
        }

        public void SetDiagonalDown(string fromAddress, string toAddress, int lineWeight)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.Borders[Excel.XlBordersIndex.xlDiagonalDown].Weight = lineWeight;
            
        }

        public void SetDiagonalDown(string fromAddress, string toAddress, int lineWeight, System.Drawing.Color color)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.Borders[Excel.XlBordersIndex.xlDiagonalDown].Weight = lineWeight;
            _excelRange.Borders[Excel.XlBordersIndex.xlDiagonalDown].Color = System.Drawing.ColorTranslator.ToOle(color);
        }

        public void SetDiagonalDown_Up(string fromAddress, string toAddress, int lineWeight)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.Borders[Excel.XlBordersIndex.xlDiagonalDown].Weight = lineWeight;
            _excelRange.Borders[Excel.XlBordersIndex.xlDiagonalUp].Weight = lineWeight;
        }

        public void SetFontProperty(string fromAddress, string toAddress, bool isBold, bool isItalic, bool isStikeThrough, 
            bool isUnderline, bool isSubscript, bool isSuperscript)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.Font.Bold = isBold;
            _excelRange.Font.Italic = isItalic;
            _excelRange.Font.Strikethrough = isStikeThrough;
            _excelRange.Font.Underline = isUnderline;
            _excelRange.Font.Subscript = isSubscript;
            _excelRange.Font.Superscript = isSuperscript;
        }

        public void SetTextColor(string fromAddress, string toAddress, System.Drawing.Color color)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.Font.Color = System.Drawing.ColorTranslator.ToOle(color);
        }

        public void SetBorders(string fromAddress, string toAddress, int lineWeight)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.Borders.LineStyle = Excel.Constants.xlSolid;
            _excelRange.Borders.Weight = lineWeight;
        }


        public void SetNonBorders(string fromAddress, string toAddress)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.Borders.LineStyle = Excel.Constants.xlNone;
            //_excelRange.Borders.Weight = lineWeight;
        }

        public void SetOulineBorders(string fromAddress, string toAddress, int lineWeight)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.Constants.xlSolid;
            _excelRange.Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = lineWeight;

            _excelRange.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.Constants.xlSolid;
            _excelRange.Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = lineWeight;

            _excelRange.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.Constants.xlSolid;
            _excelRange.Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = lineWeight;

            _excelRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.Constants.xlSolid;
            _excelRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = lineWeight;
        }

        public void SetBackgroundPatternColor(string fromAddress, string toAddress, System.Drawing.Color color)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.Interior.Pattern = Excel.XlPattern.xlPatternSolid;
            _excelRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(color);
        }

        public void SetAlign(string fromAddress, string toAddress, Excel.XlHAlign horizontalAlign, Excel.XlVAlign verticalAlign)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.HorizontalAlignment = horizontalAlign;
            _excelRange.VerticalAlignment = verticalAlign;
        }


        public void MergeCells(string fromAddress, string toAddress, bool autoAlignCenter)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.MergeCells = true;
            if (autoAlignCenter)
            {
                _excelRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
        }

        public void SetOrientation(string fromAddress, string toAddress, Excel.XlOrientation orientation)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.Orientation = orientation;
        }



        public void SetWrapText(string fromAddress, string toAddress, bool isWrapText)
        {
            _excelRange = _excelSheet.get_Range(fromAddress, toAddress);
            _excelRange.WrapText = isWrapText;
        }

        #region Column
      

        public void SetColumnWidth(string beginCol, string endCol, double width)
        {
            _excelRange = _excelSheet.get_Range(beginCol + "1", endCol + "1");
            _excelRange.EntireColumn.ColumnWidth = width;
        }

        public void SetRowHight(string beginRow, string endRow, double hight)
        {
            _excelRange = _excelSheet.get_Range(beginRow, endRow );
            _excelRange.EntireRow.RowHeight = hight;
        }

        public void SetColumnAutoFit(string beginCol, string endCol)
        {
            _excelRange = _excelSheet.get_Range(beginCol + "1", endCol + "1");
            _excelRange.EntireColumn.AutoFit();
        }

        /// <summary>
        /// AutoFitColumnAllCells
        /// </summary>
        public void AutoFitColumnAllCells()
        {
            string strcolumnwith = System.Configuration.ConfigurationManager.AppSettings["ColumnWith"];
            string strfitToPagesWide = System.Configuration.ConfigurationManager.AppSettings["fitToPagesWide"];
            string strfitToPagesTall = System.Configuration.ConfigurationManager.AppSettings["fitToPagesTall"];
            int fitToPagesWide = int.Parse(strfitToPagesWide);
            int fitToPagesTall = int.Parse(strfitToPagesTall);
            double columnwith = double.Parse(strcolumnwith);
            //Auto fit columns all cells
            SetColumnWidthAndAuto_ByMin("A", "BU", columnwith);
            //Page setup 
            PageSetupForPrinting_WideAnddTall(fitToPagesWide, fitToPagesTall);
        }
        //DongTC
        public void WrapTextFromAddressToAddress()
        { 
            SetWrapText("E5", "E7", true);
        }



        public void SetColumnWidthAndAuto_ByMin(string beginCol, string endCol, double width)
        {
            int beginColIndex = CalculateExcelColumnIndex_Number(beginCol);
            int endColIndex = CalculateExcelColumnIndex_Number(endCol);

            if (beginColIndex > endColIndex)
            {
                int temp = beginColIndex;
                beginColIndex = endColIndex;
                endColIndex = beginColIndex;
            }

            for (int i = beginColIndex; i <= endColIndex; i++)
            {
                string strColIndex = CalculateExcelColumnIndex_Alphabet(i);

                _excelRange = _excelSheet.get_Range(strColIndex + "1", strColIndex + "1");
                _excelRange.EntireColumn.AutoFit();

                if ((double)_excelRange.EntireColumn.ColumnWidth > width)
                {
                    _excelRange.EntireColumn.ColumnWidth = width;
                }
            }
        }

        #endregion Column

        #endregion Format

        #region Sheet

        public bool IsContainsSheetByName(string sheetname)
        {
            for (int i = 1; i <= _excelSheets.Count; i++)
            {
                Excel._Worksheet sheet = (Excel._Worksheet)(_excelSheets.get_Item(i));

                if (sheet.Name.Trim().ToUpper() == sheetname.Trim().ToUpper())
                {
                    return true;
                }
            }

            return false;
        }

        public void SelectSheet(int sheetIndex, bool isActivate)
        {
            _excelSheet = (Excel._Worksheet)(_excelSheets.get_Item(sheetIndex + 1));
            
            if (isActivate)
            {
                _excelSheet.Activate();
            }
        }

        public bool SelectSheetByName(string sheetname, bool isActivate)
        {
            bool isFound = false;
            Excel._Worksheet sheet = null;
            
            for (int i = 1; i <= _excelSheets.Count; i++)
            {
                sheet = (Excel._Worksheet)(_excelSheets.get_Item(i));

                if (sheet.Name.Trim().ToUpper() == sheetname.Trim().ToUpper())
                {
                    isFound = true;
                    _excelSheet = sheet;
                    break;
                }
            }

            if (isFound && isActivate)
            {
                _excelSheet.Activate();
            }

            return isFound;
        }

        public void SetSheetName(string name)
        {
            _excelSheet.Name = name;
        }

        public void AddSheetAtLast(bool isActivate)
        {
            Excel._Worksheet lastSheet = (Excel._Worksheet)(_excelSheets.get_Item(_excelSheets.Count));

            _excelSheets.Add(_value, lastSheet, 1, _value);

            //If not active new sheet, re-active current sheet
            if (!isActivate)
            {
                _excelSheet.Activate();
            }

            lastSheet = null;
        }

        public void AddSheetAfterCurrent(bool isActivate)
        {
            _excelSheets.Add(_value, _value, 1, _value);

            //If not active new sheet, re-active current sheet
            if (!isActivate)
            {
                _excelSheet.Activate();
            }
        }

        public void AddSheetAtIndex(int index, bool isActivate)
        {
            if (index < 0)
            {
                return;
            }

            if (index >= _excelSheets.Count)
            {
                AddSheetAtLast(isActivate);
                return;
            }

            Excel._Worksheet tempSheet = null;

            tempSheet = (Excel._Worksheet)(_excelSheets.get_Item(index+1));
            _excelSheets.Add(tempSheet, _value, 1, _value);
            if (!isActivate)
            {
                _excelSheet.Activate();
            }
            tempSheet = null;
        }

        /// <summary>
        /// Remove all worksheet except the last one
        /// </summary>
        public void ClearAllSheets()
        {
            Excel._Worksheet tempSheet;
            int sheetCount = _excelSheets.Count;
            for (int i = sheetCount - 1; i > 0; i--)
			{
                tempSheet = (Excel._Worksheet)(_excelSheets.get_Item(i));

                tempSheet.Delete();
			}
        }

        #endregion Sheet

        #region Range
        public Excel.Range GetRange(string fromAddress, string toAddress)
        {
            return _excelSheet.get_Range(fromAddress, toAddress);
        }
        #endregion Range

        #region Save file

        public void SaveToFile(string filePath)
        {
            _workBook.SaveAs(filePath, _value, _value, _value, _value, _value, Excel.XlSaveAsAccessMode.xlExclusive, _value, _value, _value, _value, _value);
        }

        #endregion Save file

        #region Others methods
        public static int GetExcelColor(int red, int green, int blue)
        {
            return blue * 256 * 256 + green * 256 + red;
        }
        
        public static string CalculateExcelColumnIndex_Alphabet(int colIndex)
        {
            if (colIndex < 26)
            {
                return ((char)(65 + colIndex)).ToString();
            }
            else // >=26
            {
                if (colIndex <= MAX_COL_INDEX)
                {
                    int divValue = colIndex / 26;
                    int modValue = colIndex % 26;

                    return ((char)(65 + (divValue - 1))).ToString() + (char)(65 + modValue);
                }
                else
                {
                    return "Cannot convert!";
                }
            }
        }

        private int CalculateExcelColumnIndex_Number(string strIndex)
        {
            //Const = 26

            int result = 0;

            char[] chrs = strIndex.ToUpper().ToCharArray();

            //Reverse the chars array
            Array.Reverse(chrs);

            for (int i = 0; i < chrs.Length; i++)
            {
                int chrCode = (int)chrs[i];

                if (chrCode < 65 || chrCode > 90)
                {
                    return 0;
                }

                result = result + (int)Math.Pow(26, i) * (chrCode - 65 + 1);
            }
            result--;

            return result;
        }

        public static string GetExcelAddress(int colIndex, int rowIndex)
        {
            return CalculateExcelColumnIndex_Alphabet(colIndex) + (rowIndex + 1).ToString();
        }
        #endregion Others methods

        #region Private Methods

        private string GetArrayValueAsString(object[,] arr, int rowIndex, int colIndex, string defaultValue)
        {
            if (arr[rowIndex, colIndex] != null)
            {
                return arr[rowIndex, colIndex].ToString();
            }

            return defaultValue;
        }

        private void NAR(object o)
        {

            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            catch
            {
            }
            finally
            {
                o = null;
            }
        }
        #endregion Private Methods

        #region Page setup

        public void PageSetupForPrinting_WideAnddTall(int fitToPagesWide, int fitToPagesTall)
        {
            _excelSheet.PageSetup.Zoom = false;
            _excelSheet.PageSetup.FitToPagesWide = fitToPagesWide;
            _excelSheet.PageSetup.FitToPagesTall = fitToPagesTall;
        }

        public void PageSetupForPrinting_PrintArea(string printArea)
        {
            _excelSheet.PageSetup.PrintArea = printArea;
        }

        public void PageSetupForPrinting_Zoom(double zoom)
        {
            _excelSheet.PageSetup.Zoom = zoom;
        }

        #endregion Page setup
    }
}
