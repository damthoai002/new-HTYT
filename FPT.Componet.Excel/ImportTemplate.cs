using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace FPT.Component.ExcelPlus
{
    public class ImportTemplate
    {
        public string Name { get; set; }
        [XmlArrayItem("Sheet"), XmlArray("SheetCollection")]
        public List<SheetTemplate> SheetCollection { get; set; }
        public bool UseSheetIndex { get; set; }

        public static ImportTemplate Load(string filePath)
        {
            ImportTemplate result = new ImportTemplate();
            XmlSerializer xmlSer = new XmlSerializer(typeof(ImportTemplate));
            object obj = null;
            using (StreamReader wr = new StreamReader(filePath))
            {
                obj = xmlSer.Deserialize(wr);
            }
            if (obj != null)
            {
                result = obj as ImportTemplate;
            }
            return result;
        }

        public int GetMaxSheetIndex()
        {
            int result = -1;
            if (SheetCollection != null)
            {
                result = SheetCollection.Count;
                foreach (SheetTemplate sheet in SheetCollection)
                {
                    if (result < sheet.Index) result = sheet.Index;
                }
            }
            return result;
        }

        public ImportTemplate()
        {
            Name = string.Empty;
            SheetCollection = new List<SheetTemplate>();
            UseSheetIndex = true;
        }
    }

    public class SheetTemplate
    {
        public string Name { get; set; }
        public int Index { get; set; }
        [XmlArrayItem("Property"), XmlArray("HeaderTemplate")]
        public List<PropertyCell> Header { get; set; }
        [XmlArrayItem("Label"), XmlArray("LabelCollection")]
        public List<LabelCell> LabelCollection { get; set; }
        public TableTemplate Table { get; set; }
        public SheetTemplate()
        {
            Name = string.Empty;
            Index = -1;
            Header = new List<PropertyCell>();
            Table = new TableTemplate();
            LabelCollection = new List<LabelCell>();
        }
    }

    public class PropertyCell
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string DataFormat { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }

        public Type DataType
        {
            get
            {
                return Type.GetType(TypeName);
            }
        }

        public PropertyCell()
        {
            Name = string.Empty;
            TypeName = typeof(string).FullName;
            DataFormat = string.Empty;
            Column = -1;
            Row = -1;
        }
    }

    public class LabelCell
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }

        public LabelCell()
        {
            Name = string.Empty;
            Column = -1;
            Row = -1;
            Value = string.Empty;
        }
    }

    public class TableTemplate
    {
        public string Name { get; set; }
        public int StartRow { get; set; }
        public int StartColumn { get; set; }
        public int HeaderIndex { get; set; }
        [XmlArrayItem("Column"), XmlArray("ColumnCollection")]
        public List<ColumnTemplate> ColumnCollection { get; set; }

        /// <summary>
        /// Get list of primary key column (DB name) in template
        /// </summary>
        /// <returns></returns>
        public List<string> GetPrimaryKeys()
        {
            List<string> result = new List<string>();
            if (ColumnCollection != null)
            {
                foreach (ColumnTemplate item in ColumnCollection)
                {
                    if (item.IsKey)
                        result.Add(item.DBName);
                }
            }
            return result;
        }

        public TableTemplate()
        {
            Name = string.Empty;
            StartColumn = -1;
            StartRow = -1;
            ColumnCollection = new List<ColumnTemplate>();
            HeaderIndex = -1;
        }

        public System.Data.DataTable BuildSchema()
        {
            System.Data.DataTable result = new System.Data.DataTable();
            result.TableName = Name;
            foreach (ColumnTemplate col in ColumnCollection)
            {
                result.Columns.Add(col.DBName, col.DataType);
            }
            return result;
        }
    }

    public class ColumnTemplate
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string DataFormat { get; set; }
        public int Index { get; set; }
        public string DBName { get; set; }
        public bool IsKey { get; set; }

        public Type DataType
        {
            get
            {
                return Type.GetType(TypeName);
            }
        }

        public ColumnTemplate()
        {
            Name = string.Empty;
            TypeName = typeof(string).FullName;
            DataFormat = string.Empty;
            Index = -1;
            DBName = string.Empty;
            IsKey = false;
        }
    }
}
