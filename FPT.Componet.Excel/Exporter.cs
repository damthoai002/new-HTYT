using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPT.Component.ExcelPlus
{
    public abstract class Exporter
    {
        public const string DEFAULT_TABLE_NAME = "Export";
        public const string EXCEL2003_EXTENSION = ".XLS";
        public const string EXCEL2007_EXTENSION = ".XLSX";
        #region Properties
        protected string currentFile;
        protected IExcelWriter writer2k3;
        protected IExcelWriter writer2k7;
        #endregion Properties

        #region Abstract Methods
        protected abstract IList<System.Data.DataTable> ExportTableCollection { get; }
        protected abstract IErrorLogger Logger { get; }
        #endregion Abstract Methods

        #region Virtual methods
        protected virtual IList<SizeFormat> RowHeightCollection
        {
            get { return new List<SizeFormat>(); }
        }

        protected virtual IList<SizeFormat> ColumnWidthCollection
        {
            get { return new List<SizeFormat>(); }
        }

        protected virtual IList<FRangeAddress> GetMergeCellCollection(int sheetNo)
        {
            return new List<FRangeAddress>();
        }

        protected virtual IList<RangesFormat<CellStyle>> GetRangeFormatCollection(int sheetNo)
        {
            return new List<RangesFormat<CellStyle>>();
        }

        protected virtual IExcelWriter Writer
        {
            get
            {

                ExcelVersion version = GetVersion(currentFile);
                if (version == ExcelVersion.Excel2003)
                {
                    if (writer2k3 == null)
                    {
                        writer2k3 = ExcelFactory.CreateExcelWriter(ExcelVersion.Excel2003);
                    }
                    return writer2k3;
                }
                else
                {
                    if (writer2k7 == null)
                    {
                        writer2k7 = ExcelFactory.CreateExcelWriter(ExcelVersion.Excel2007);
                    }
                    return writer2k7;
                }
            }
        }
        #endregion Virtual methods
        public Exporter()
        {
            writer2k3 = null;
            writer2k7 = null;
        }

        protected virtual ExcelVersion GetVersion(string filePath)
        {
            string ext = System.IO.Path.GetExtension(filePath).ToUpper();
            if (ext.Equals(EXCEL2003_EXTENSION))
            {
                return ExcelVersion.Excel2003;
            }
            return ExcelVersion.Excel2007;
        }

        public bool Export(string filePath)
        {
            bool result = true;
            currentFile = filePath;
            try
            {
                result = Writer.CreateWorkBook(filePath);
                if (!result) return result;
                result = Writer.WriteToWorkBook(ExportTableCollection);

                if (!result) return result;
                result = RenderStyle();
            }
            catch (Exception ex)
            {
                result = false;
                Logger.LogException(ex);
            }
            finally
            {
                Writer.CloseWorkBook();
            }
            return result;
        }

        protected bool RenderStyle()
        {
            bool result = true;
            try
            {
                for (int i = 0; i < ExportTableCollection.Count; i++)
                {
                    IList<RangesFormat<CellStyle>> rangeFormats = GetRangeFormatCollection(i + 1);

                    foreach (var item in rangeFormats)
                    {
                        result = result && Writer.SetCellStyle(item.Ranges, item.Format, i + 1);
                    }
                    IList<FRangeAddress> mergeCells = GetMergeCellCollection(i + 1);
                    result = result && Writer.MergeCells(mergeCells, i + 1);
                }
                result = result && Resize();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                result = false;
            }
            return result;
        }

        protected bool Resize()
        {
            bool result = true;
            try
            {
                IList<SizeFormat> rowHeightList = RowHeightCollection;
                IList<SizeFormat> columnWidth = ColumnWidthCollection;
                foreach (SizeFormat s in rowHeightList)
                {
                    if (s.Size > 0)
                    {
                        result = result && Writer.SetRowHeight(s.SheetNumber, s.Range, s.Size);
                    }
                }

                foreach (SizeFormat s in columnWidth)
                {
                    if (s.AutoSize)
                    {
                        result = result && Writer.SetAutoWidthColumn(s.SheetNumber, s.Range);
                    }
                    else
                    {
                        if (s.Size > 0)
                        {
                            result = result && Writer.SetColumnWidth(s.SheetNumber, s.Range, s.Size);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Logger.LogException(ex);
            }
            return result;
        }
    }

    public class RangesFormat<T>
    {
        private IList<FRangeAddress> ranges;
        public IList<FRangeAddress> Ranges { get { return ranges; } }

        public T Format { get; set; }

        public RangesFormat()
        {
            ranges = new List<FRangeAddress>();
            Format = Activator.CreateInstance<T>();
        }
    }

    public class SizeFormat
    {
        public int Size { get; set; }
        public bool AutoSize { get; set; }
        public int SheetNumber { get; set; }
        public IList<int> Range { get; set; }

        public SizeFormat()
        {
            Size = -1;
            AutoSize = false;
            SheetNumber = -1;
            Range = new List<int>();
        }

    }
}
