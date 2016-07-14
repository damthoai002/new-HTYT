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
    public class TonKhoExporter : Exporter
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

        public TonKhoExporter(bool exportFailResult)//, TimePeriod timePeriod
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
            dtSchema.TableName = "TonKhoExpoter";

            // Add first blank colum
            dtSchema.Columns.Add("BLANK1");

            // Add columns of store's information
            dtSchema.Columns.Add(TonKhoExportColumns.TenThuoc);
            dtSchema.Columns.Add(TonKhoExportColumns.ThanhPhanThuoc);
            dtSchema.Columns.Add(TonKhoExportColumns.HamLuong);
            dtSchema.Columns.Add(TonKhoExportColumns.GPNK);
            dtSchema.Columns.Add(TonKhoExportColumns.DangBaoChe);
            dtSchema.Columns.Add(TonKhoExportColumns.NhaSanXuat);
            dtSchema.Columns.Add(TonKhoExportColumns.QuocGia);
            dtSchema.Columns.Add(TonKhoExportColumns.DonViTinh);
            dtSchema.Columns.Add(TonKhoExportColumns.MaKho);
            dtSchema.Columns.Add(TonKhoExportColumns.TenKho);
            dtSchema.Columns.Add(TonKhoExportColumns.HoatDong);
            dtSchema.Columns.Add(TonKhoExportColumns.SoLuong);
            dtSchema.Columns.Add(TonKhoExportColumns.LoThuoc);
            dtSchema.Columns.Add(TonKhoExportColumns.HanSuDung);




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
            //drDataTemp = dtResult.NewRow();
            //drDataTemp[TonKhoExportColumns.MaThuoc] = "Danh sách thuôc xuất kho ngày " + DateTime.Now.ToString("dd/MM/yyyy");
            //dtResult.Rows.Add(drDataTemp);

            // Add first blank row
            drDataTemp = dtResult.NewRow();
            dtResult.Rows.Add(drDataTemp);


            // Add Colum Header
            drDataTemp = dtResult.NewRow();
            drDataTemp[TonKhoExportColumns.TenThuoc] = "Tên Thuốc";
            drDataTemp[TonKhoExportColumns.ThanhPhanThuoc] = "Thành Phần";
            drDataTemp[TonKhoExportColumns.HamLuong] = "Hàm Lượng";
            drDataTemp[TonKhoExportColumns.GPNK] = "Số Đăng Ký Hoặc GPNK";
            drDataTemp[TonKhoExportColumns.DangBaoChe] = "Dạng bào chế/Đường dùng";
            drDataTemp[TonKhoExportColumns.NhaSanXuat] = "Nhà Sản Xuất";
            drDataTemp[TonKhoExportColumns.QuocGia] = "Quốc Gia";
            drDataTemp[TonKhoExportColumns.DonViTinh] = "Đơn Vị Tính";
            drDataTemp[TonKhoExportColumns.MaKho] = "Mã Kho";
            drDataTemp[TonKhoExportColumns.TenKho] = "Kho";
            drDataTemp[TonKhoExportColumns.HoatDong] = "Hoạt Động";
            drDataTemp[TonKhoExportColumns.SoLuong] = "Số Lượng";
            drDataTemp[TonKhoExportColumns.LoThuoc] = "Lô Thuốc";
            drDataTemp[TonKhoExportColumns.HanSuDung] = "Hạn Sử Dụng";

            





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



                drDataTemp[TonKhoExportColumns.TenThuoc] = dr[TonKhoExportColumns.TenThuoc];
                drDataTemp[TonKhoExportColumns.ThanhPhanThuoc] = dr[TonKhoExportColumns.ThanhPhanThuoc];
                drDataTemp[TonKhoExportColumns.HamLuong] = dr[TonKhoExportColumns.HamLuong];
                drDataTemp[TonKhoExportColumns.GPNK] = dr[TonKhoExportColumns.GPNK];
                drDataTemp[TonKhoExportColumns.DangBaoChe] = dr[TonKhoExportColumns.DangBaoChe];
                drDataTemp[TonKhoExportColumns.NhaSanXuat] = dr[TonKhoExportColumns.NhaSanXuat];
                drDataTemp[TonKhoExportColumns.QuocGia] = dr[TonKhoExportColumns.QuocGia];
                drDataTemp[TonKhoExportColumns.DonViTinh] = dr[TonKhoExportColumns.DonViTinh];
                drDataTemp[TonKhoExportColumns.MaKho] = dr[TonKhoExportColumns.MaKho];
                drDataTemp[TonKhoExportColumns.TenKho] = dr[TonKhoExportColumns.TenKho];
                drDataTemp[TonKhoExportColumns.HoatDong] = dr[TonKhoExportColumns.HoatDong];
                drDataTemp[TonKhoExportColumns.SoLuong] = dr[TonKhoExportColumns.SoLuong];
                drDataTemp[TonKhoExportColumns.LoThuoc] = dr[TonKhoExportColumns.LoThuoc];
                drDataTemp[TonKhoExportColumns.HanSuDung] = dr[TonKhoExportColumns.HanSuDung];
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
            RangesFormat<CellStyle> format;

            // Edit Store
            //format = new RangesFormat<CellStyle>();
            //format.Ranges.Add( new FRangeAddress(2, 2, 2, 4));
            //format.Format.BackGroundColor = Color.FromArgb(128, 0, 0);
            //format.Format.TextStyle.TextColor = Color.White;
            //format.Format.TextStyle.Bold = true;
            //result.Add(format);

            // Channel
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



            //// Bolder "Extra Display Set"
            //format = new RangesFormat<CellStyle>();

            //format.Ranges.Add(new FRangeAddress(9, _columnBeginOfExtraDS, 9, _columnEndOfExtraDS));
            //format.Format.Border.Top = bdStyle;
            //format.Format.Border.Left = bdStyle;
            //format.Format.Border.Right = bdStyle;
            //format.Format.Border.Bottom = bdStyle;
            //format.Format.HAlign = FHorizontalAlignment.Center;
            //format.Format.TextStyle.Bold = true;
            //result.Add(format);

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
