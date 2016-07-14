using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FPT.Component.ExcelPlus
{
    public class CsvMapping
    {
        const string XML_ROOT = "CsvTable";
        const string XML_HEADERINDEX = "HeaderIndex";
        const string XML_COLUMNCOLLECTION = "ColumnCollection";
        const string XML_COLUMN = "Column";
        const string XML_COLINDEX = "Index";
        const string XML_CSVNAME = "CsvName";
        const string XML_COLNAME = "Name";
        const string XML_DATATYPE = "DataType";
        const string XML_FORMAT = "format";

        protected List<CsvColumnDefinition> columnCollection;

        public int HeaderIndex { get; set; }

        public IList<CsvColumnDefinition> ColumnCollection
        {
            get
            {
                return columnCollection;
            }
        }

        public void SortColumn()
        {
            columnCollection.Sort((x,y)=>x.Index.CompareTo(y.Index));
        }

        public CsvMapping Copy()
        {
            CsvMapping result = new CsvMapping();
            result.HeaderIndex = HeaderIndex;
            foreach (var item in columnCollection)
            {
                result.ColumnCollection.Add(new CsvColumnDefinition(item));
            }
            return result;
        }

        public CsvMapping()
        {
            HeaderIndex = 0;
            columnCollection = new List<CsvColumnDefinition>();
        }

        public void UpdateIndex(string[] header)
        {
            List<string> tmp = new List<string>(header);
            foreach (CsvColumnDefinition col in columnCollection)
            {
                if (col.Index < 0)
                {
                    col.Index = tmp.FindIndex(x => x.ToUpper() == col.CsvName.ToUpper());
                }
            }
        }

        public static CsvMapping Load(string mappingFilePath)
        {
            XDocument doc = XDocument.Load(mappingFilePath);
            XElement root = doc.Element(XML_ROOT);
            CsvMapping result = new CsvMapping();
            if (root != null)
            {
                XElement hIndex = root.Element(XML_HEADERINDEX);
                if (hIndex != null)
                    result.HeaderIndex = IndexParse(hIndex.Value);
                XElement colCollection = root.Element(XML_COLUMNCOLLECTION);
                foreach (XElement col in colCollection.Elements(XML_COLUMN))
                {
                    XElement name = col.Element(XML_COLNAME);
                    if (name != null)
                    {
                        CsvColumnDefinition column = new CsvColumnDefinition();
                        column.Name = name.Value;
                        XElement csvName = col.Element(XML_CSVNAME);
                        if (csvName != null) column.CsvName = csvName.Value;
                        XElement index = col.Element(XML_COLINDEX);
                        if (index != null) column.Index = IndexParse(index.Value);
                        XElement type = col.Element(XML_DATATYPE);
                        if (type != null)
                        {
                            column.ColumnType.DataType = Type.GetType(type.Value);
                            XAttribute format = type.Attribute(XML_FORMAT);
                            if (format != null) column.ColumnType.Format = format.Value;
                        }
                        result.ColumnCollection.Add(column);
                    }
                }
            }
            return result;
        }

        private static int IndexParse(string value)
        {
            try
            {
                return int.Parse(value);
            }
            catch
            {
                return -1;
            }
        }
    }

    public class CsvColumnDefinition
    {
        public int Index { get; set; }
        public string CsvName { get; set; }
        public string Name { get; set; }
        public DataFormat ColumnType { get; set; }

        public CsvColumnDefinition()
        {
            Index = -1;
            CsvName = string.Empty;
            Name = string.Empty;
            ColumnType = new DataFormat();
        }

        public CsvColumnDefinition(CsvColumnDefinition other)
        {
            Index = other.Index;
            CsvName = other.CsvName;
            Name = other.Name;
            ColumnType = new DataFormat(other.ColumnType);
        }
    }

    public class DataFormat
    {
        public Type DataType { get; set; }
        public string Format { get; set; }

        public DataFormat()
        {
            DataType = typeof(string);
            Format = string.Empty;
        }

        public DataFormat(DataFormat other)
        {
            DataType = other.DataType;
            Format = other.Format;
        }
    }
}
