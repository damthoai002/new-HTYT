using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPT.Component.ExcelPlus
{
    public class ImportSheet
    {
        protected Dictionary<string, object> properties;
        protected System.Data.DataTable table;

        public string SheetName { get; set; }
        public int Index { get; set; }
        public SheetTemplate Template { get; set; }

        public System.Data.DataTable Table
        {
            get
            {
                return table;
            }
        }

        public Dictionary<string, object> GetAllProperties()
        {
            return properties;
        }

        public object GetProperty(string propertyName)
        {
            if (properties.ContainsKey(propertyName))
                return properties[propertyName];
            return null;
        }

        public ImportSheet()
        {
            properties = new Dictionary<string, object>();
            table = new System.Data.DataTable();
            SheetName = string.Empty;
            Index = 0;
            Template = null;
        }

        public static ImportSheet Load(ISheet dataSheet, SheetTemplate template)
        {
            ImportSheet result = new ImportSheet();
            result.properties = new Dictionary<string, object>();
            result.table = template.Table.BuildSchema();
            result.SheetName = dataSheet.SheetName;
            result.Index = dataSheet.SheetNumber;
            result.Template = template;
            foreach (PropertyCell p in template.Header)
            {
                string value = dataSheet.Cells[p.Row, p.Column];
                object obj = Utility.ChangeType(value, p.DataType, p.DataFormat);
                if (result.properties.ContainsKey(p.Name))
                    result.properties[p.Name] = obj;
                else
                    result.properties.Add(p.Name, obj);
            }
            if (template.Table.HeaderIndex > 0)
            {
                foreach (ColumnTemplate col in template.Table.ColumnCollection)
                {
                    for (int i = dataSheet.Cells.StartColumn; i <= dataSheet.Cells.EndColumn; i++)
                    {
                        if (dataSheet.Cells[template.Table.HeaderIndex, i].ToUpper().Trim().Equals(col.Name))
                            col.Index = i;
                    }
                }
            }
            
            for (int i = template.Table.StartRow; i <= dataSheet.Cells.EndRow; i++)
            {
                System.Data.DataRow row = result.table.NewRow();
                foreach (ColumnTemplate col in template.Table.ColumnCollection)
                {
                    row[col.DBName] = dataSheet.Cells[i, col.Index];
                }
                result.table.Rows.Add(row);
            }
            return result;
        }
    }
}
