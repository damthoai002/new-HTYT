using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using FPT.Component.ExcelPlus;
using UKPI.DataAccessObject;
using UKPI.Utils;
using System.Drawing;
using UKPI.ValueObject;

namespace UKPI.BusinessObject
{
    public class XuatKhoExporter : Exporter
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(Logger));
        private IErrorLogger _logger = new Logger(log);
        private IList<System.Data.DataTable> _exportTables = new List<DataTable>();

        private string _channel;
        private bool _exportFailResult;

        private int _columnBeginOfBasicDS = 0;
        private int _columnEndOfBasicDS = 0;
        private int _columnBeginOfExtraDS = 0;
        private int _columnEndOfExtraDS = 0;
        private int _countNoOfRow = 0;
     //   private TimePeriod _currentTimePeriod;
      
      //  private List<ExtraDisplaySetBO> _extraDisplaySetList;

        public XuatKhoExporter(bool exportFailResult)//, TimePeriod timePeriod
            : base()
        {
          //  _currentTimePeriod = timePeriod;
          //  _basicDisplaySetList = BasicDisplaySetBO.GetAllActiveBasicDisplaySet();//timePeriod
          //  _extraDisplaySetList = ExtraDisplaySetBO.GetAllActiveExtraDisplaySet(timePeriod);

            _columnBeginOfBasicDS = 21;
            //_columnEndOfBasicDS = _columnBeginOfBasicDS + _basicDisplaySetList.Count - 1;
            _columnBeginOfExtraDS = _columnEndOfBasicDS + 1;
          //  _columnEndOfExtraDS = _columnBeginOfExtraDS + _extraDisplaySetList.Count - 1;

            _exportFailResult = exportFailResult;
        }

        protected override IList<System.Data.DataTable> ExportTableCollection
        {
            get { return _exportTables; }
        }

        protected override IErrorLogger Logger
        {
            get { return _logger; }
        }

        private DataTable GetExportTableSchema()
        {
            DataTable dtSchema = new DataTable();
            dtSchema.TableName = "XuatKhoExporte";

            // Add first blank colum
            dtSchema.Columns.Add("BLANK1");

            // Add columns of store's information
            dtSchema.Columns.Add(XuatKhoExportColumns.MaThuoc);
            dtSchema.Columns.Add(XuatKhoExportColumns.TenThuoc);
            dtSchema.Columns.Add(XuatKhoExportColumns.SoLuong);
            dtSchema.Columns.Add(XuatKhoExportColumns.XuatKho);
            dtSchema.Columns.Add(XuatKhoExportColumns.MaLoThuoc);
            dtSchema.Columns.Add(XuatKhoExportColumns.HanSuDung);
            dtSchema.Columns.Add(XuatKhoExportColumns.PhongKhamKho);
            dtSchema.Columns.Add(XuatKhoExportColumns.MaKhamBenh);
            dtSchema.Columns.Add(XuatKhoExportColumns.Status);


            // Add BasicDisplaySet columns
            //foreach (BasicDisplaySetBO basicDS in _basicDisplaySetList)
            //{
            //    dtSchema.Columns.Add(basicDS.Code);
            //}

            //// Add ExtraDisplaySet columns
            //foreach (ExtraDisplaySetBO extraDS in _extraDisplaySetList)
            //{
            //    dtSchema.Columns.Add(extraDS.Code);
            //}

            //// For export fail result of Import
            //if (_exportFailResult)
            //{
            //    dtSchema.Columns.Add(EditStoreImporter.ERROR_MESSAGE);
            //}

            return dtSchema;

        }

        private DataTable ConvertDataTable2ExportFormat(DataTable dt)
        {
            DataTable dtResult = this.GetExportTableSchema();

            _countNoOfRow = dt.Rows.Count;
            // Fill data to excel formatted table
            DataRow drDataTemp;

            #region Add title and column headers
            // Add title "Table export"
            drDataTemp = dtResult.NewRow();
            drDataTemp[XuatKhoExportColumns.MaThuoc] = "Danh sách thuôc xuất kho ngày " + DateTime.Now.ToString("dd/MM/yyyy");
            dtResult.Rows.Add(drDataTemp);

            // Add first blank row
            drDataTemp = dtResult.NewRow();
            dtResult.Rows.Add(drDataTemp);


            // Add Colum Header
            drDataTemp = dtResult.NewRow();
            drDataTemp[XuatKhoExportColumns.TenThuoc] = "Tên thuốc";
            drDataTemp[XuatKhoExportColumns.MaThuoc] = "Mã thuốc";
            drDataTemp[XuatKhoExportColumns.SoLuong] = "Số lượng";
            drDataTemp[XuatKhoExportColumns.XuatKho] = "Xuất kho";
            drDataTemp[XuatKhoExportColumns.MaLoThuoc] = "Mã lô thuốc";
            drDataTemp[XuatKhoExportColumns.HanSuDung] = "Hạn sử dụng";
            drDataTemp[XuatKhoExportColumns.PhongKhamKho] = "Tên kho thuốc";
            drDataTemp[XuatKhoExportColumns.MaKhamBenh] = "mã khám bệnh";
            drDataTemp[XuatKhoExportColumns.Status] = "Trạng thái";
            





            //foreach (BasicDisplaySetBO basicDS in _basicDisplaySetList)
            //{
            //    drDataTemp[basicDS.Code] = basicDS.Name;
            //}
            //foreach (ExtraDisplaySetBO extraDS in _extraDisplaySetList)
            //{
            //    drDataTemp[extraDS.Code] = extraDS.Name;
            //}

            dtResult.Rows.Add(drDataTemp);

            #endregion

            // Add store list
            foreach (DataRow dr in dt.Rows)
            {
                drDataTemp = dtResult.NewRow();

                drDataTemp[XuatKhoExportColumns.TenThuoc] = dr[XuatKhoExportColumns.TenThuoc];
                drDataTemp[XuatKhoExportColumns.MaThuoc] = dr[XuatKhoExportColumns.MaThuoc];
                drDataTemp[XuatKhoExportColumns.SoLuong] = dr[XuatKhoExportColumns.SoLuong];
                drDataTemp[XuatKhoExportColumns.XuatKho] = dr[XuatKhoExportColumns.XuatKho];
                drDataTemp[XuatKhoExportColumns.MaLoThuoc] = dr[XuatKhoExportColumns.MaLoThuoc];
                drDataTemp[XuatKhoExportColumns.HanSuDung] = dr[XuatKhoExportColumns.HanSuDung];
                drDataTemp[XuatKhoExportColumns.PhongKhamKho] = dr[XuatKhoExportColumns.PhongKhamKho];
                drDataTemp[XuatKhoExportColumns.MaKhamBenh] = dr[XuatKhoExportColumns.MaKhamBenh];
                drDataTemp[XuatKhoExportColumns.Status] = dr[XuatKhoExportColumns.Status];



                //foreach (BasicDisplaySetBO basicDS in _basicDisplaySetList)
                //{
                //    drDataTemp[basicDS.Code] = dr[basicDS.Code];
                //}
                //foreach (ExtraDisplaySetBO extraDS in _extraDisplaySetList)
                //{
                //    drDataTemp[extraDS.Code] = dr[extraDS.Code];
                //}


                dtResult.Rows.Add(drDataTemp);
            }

            return dtResult;
        }

        public void AddExportTable(DataTable dt)
        {
            // _channel = channel;
            _exportTables.Add(this.ConvertDataTable2ExportFormat(dt));
        }

        public void ClearExportTable()
        {
            _exportTables.Clear();
        }

        protected override IList<FRangeAddress> GetMergeCellCollection(int sheetNo)
        {
            IList<FRangeAddress> result = new List<FRangeAddress>();

            // Merge cells of "Basic Display Set" and  "Optional Display Set"
            result.Add(new FRangeAddress(9, _columnBeginOfBasicDS, 9, _columnEndOfBasicDS));
            result.Add(new FRangeAddress(9, _columnBeginOfExtraDS, 9, _columnEndOfExtraDS));

            return result;
        }

        protected override IList<RangesFormat<CellStyle>> GetRangeFormatCollection(int sheetNo)
        {
            List<RangesFormat<CellStyle>> result = new List<RangesFormat<CellStyle>>();
            //RangesFormat<CellStyle> format;

            //// Edit Store
            ////format = new RangesFormat<CellStyle>();
            ////format.Ranges.Add( new FRangeAddress(2, 2, 2, 4));
            ////format.Format.BackGroundColor = Color.FromArgb(128, 0, 0);
            ////format.Format.TextStyle.TextColor = Color.White;
            ////format.Format.TextStyle.Bold = true;
            ////result.Add(format);

            //// Channel
            //format = new RangesFormat<CellStyle>();
            //format.Ranges.Add(new FRangeAddress(1, 2, 1, 4));
            //format.Format.BackGroundColor = Color.FromArgb(153, 51, 0);
            //format.Format.TextStyle.TextColor = Color.White;
            //format.Format.TextStyle.Size = 14;
            //format.Format.TextStyle.Bold = true;
            //result.Add(format);

            ////// Time period
            ////format = new RangesFormat<CellStyle>();
            ////format.Ranges.Add(new FRangeAddress(4, 2, 4, 4));
            ////format.Format.BackGroundColor = Color.FromArgb(228, 109, 10);
            ////format.Format.TextStyle.TextColor = Color.White;
            ////format.Format.TextStyle.Bold = true;
            ////result.Add(format);

            ////// Export date
            ////format = new RangesFormat<CellStyle>();
            ////format.Ranges.Add( new FRangeAddress(5, 2, 5, 4));
            ////format.Format.BackGroundColor = Color.FromArgb(255, 204, 153);
            ////format.Format.TextStyle.TextColor = Color.Blue;
            ////result.Add(format);
            //BorderStyle bdStyle = new BorderStyle();
            //bdStyle.BorderColor = Color.Black;
            //bdStyle.Weight = BorderWeight.Thin;
            //bdStyle.Style = LineStyle.Thin;



            //// Column headers: black background
            //format = new RangesFormat<CellStyle>();
            //format.Ranges.Add(new FRangeAddress(3, 2, 3, _columnEndOfExtraDS + 3));
            //format.Format.HAlign = FHorizontalAlignment.Left;
            //format.Format.BackGroundColor = Color.White;
            //format.Format.TextStyle.TextColor = Color.Black;
            //format.Format.Border.Top = bdStyle;
            //format.Format.Border.Left = bdStyle;
            //format.Format.Border.Right = bdStyle;
            //format.Format.Border.Bottom = bdStyle;
            //format.Format.TextStyle.Bold = true;
            //format.Format.TextStyle.Underline = true;

            //result.Add(format);

            //// Define bolder style


            //// Bolder "Basic Display Set"
            //format = new RangesFormat<CellStyle>();



            //format.Ranges.Add(new FRangeAddress(4, 2, _countNoOfRow + 3, _columnEndOfExtraDS + 3));
            //format.Format.Border.Top = bdStyle;
            //format.Format.Border.Left = bdStyle;
            //format.Format.Border.Right = bdStyle;
            //format.Format.Border.Bottom = bdStyle;
            //format.Format.HAlign = FHorizontalAlignment.Center;

            //result.Add(format);



            ////// Bolder "Extra Display Set"
            ////format = new RangesFormat<CellStyle>();

            ////format.Ranges.Add(new FRangeAddress(9, _columnBeginOfExtraDS, 9, _columnEndOfExtraDS));
            ////format.Format.Border.Top = bdStyle;
            ////format.Format.Border.Left = bdStyle;
            ////format.Format.Border.Right = bdStyle;
            ////format.Format.Border.Bottom = bdStyle;
            ////format.Format.HAlign = FHorizontalAlignment.Center;
            ////format.Format.TextStyle.Bold = true;
            ////result.Add(format);

            return result;
        }

        protected override IList<SizeFormat> ColumnWidthCollection
        {
            get
            {
                IList<SizeFormat> columnWidth = new List<SizeFormat>();
                SizeFormat size = new SizeFormat();
                size.AutoSize = true;
                size.SheetNumber = 1;

                // Set all columns as auto size
                for (int i = 1; i <= _columnEndOfExtraDS + 2; i++)
                {
                    size.Range.Add(i);
                }

                columnWidth.Add(size);

                return columnWidth;
            }
        }
    }
}
