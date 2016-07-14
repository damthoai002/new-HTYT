using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace FPT.Component.ExcelPlus
{
    public class CsvUtility
    {
        public static DataTable CsvToDataTable(string csvFilePath, string csvToTableMappingFilePath)
        {
            CsvMapping mapping = CsvMapping.Load(csvToTableMappingFilePath);
            return CsvToDataTable(csvFilePath, mapping);
        }

        public static DataTable CsvToDataTable(string csvFilePath, CsvMapping mapping)
        {
            string[][] csvData = ParseCSV(csvFilePath);
            int rowCount = csvData.Count();
            DataTable result = new DataTable();
            foreach (CsvColumnDefinition col in mapping.ColumnCollection)
            {
                result.Columns.Add(col.Name, col.ColumnType.DataType);
            }
            if (rowCount > mapping.HeaderIndex)
            {
                string[] header = csvData[mapping.HeaderIndex];
                mapping.UpdateIndex(header);
                for (int i = mapping.HeaderIndex + 1; i < rowCount; i++)
                {
                    DataRow row = result.NewRow();
                    foreach (CsvColumnDefinition col in mapping.ColumnCollection)
                    {
                        row[col.Name] = Utility.ChangeType(csvData[i][col.Index], col.ColumnType.DataType, col.ColumnType.Format);
                    }
                    result.Rows.Add(row);
                }
            }
            return result;
        }

        public static string[][] ParseCSV(TextReader inputCSV)
        {
            CSVReader reader = new CSVReader(inputCSV);
            IList<String[]> result = new List<String[]>();
            while (!reader.EOF)
            {
                String[] tmp = reader.ReadNextRecord();
                if (tmp == null)
                {
                    break;
                }
                else
                {
                    result.Add(tmp);
                }
            }
            return result.ToArray();
        }

        public static string[][] ParseCSV(string csvFilePath)
        {
            using (StreamReader sr = new StreamReader(csvFilePath))
            {
                return ParseCSV(sr);
            }
        }
    }
}
