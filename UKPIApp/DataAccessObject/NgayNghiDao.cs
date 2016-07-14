
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.DataAccessObject
{
    public class NgayNghiDao : clsBaseDAO
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(typeof(NgayNghiDao));
        private static readonly string HUFS_SelectNgayNghi = "HUFS_SelectNgayNghi";
        private static readonly string CapNhatNgayNghi = "CapNhatNgayNghi";

        public void NhapNgaynghi(List<NgayNghiKhamBenh> lstnnkb)
        {
            try
            {
                this.BulkInsert(ConvertToDataTable(lstnnkb), "HUFS_NGAYNGHI");

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message,ex);
               // throw;
            }
        }
        public DataTable GetNgayNghi()
        {
            try
            {
                return  DataServices.ExecuteDataTable(CommandType.StoredProcedure, HUFS_SelectNgayNghi);
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message,ex);
                return new DataTable();
            }
        }
        public void UpdateNgayNghi(NgayNghiKhamBenh nnkb)
        {
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[14];
                sqlParams[0] = new SqlParameter("@SysId", nnkb.SysId);
                sqlParams[1] = new SqlParameter("@MaNv", nnkb.MaNv);
                sqlParams[2] = new SqlParameter("@TenNV", nnkb.TenNV);
                sqlParams[3] = new SqlParameter("@GioiTinh", nnkb.GioiTinh);
                sqlParams[4] = new SqlParameter("@NgayNghiTu", nnkb.NgayNghiTu);
                sqlParams[5] = new SqlParameter("@NgayNghiDen", nnkb.NgayNghiDen);
                sqlParams[6] = new SqlParameter("@SoNgayNghi", nnkb.SoNgayNghi);
                sqlParams[7] = new SqlParameter("@LyDoChiTiet", nnkb.LyDoChiTiet);
                sqlParams[8] = new SqlParameter("@LyDo", nnkb.LyDo);
                sqlParams[9] = new SqlParameter("@DienGiai", nnkb.DienGiai);
                sqlParams[10] = new SqlParameter("@ChuThich", nnkb.ChuThich);
                sqlParams[11] = new SqlParameter("@NguoiTao", nnkb.NguoiTao);
                sqlParams[12] = new SqlParameter("@NgayTao", nnkb.NgayTao);
                sqlParams[13] = new SqlParameter("@IsActive", nnkb.IsActive);
             

                DataServices.ExecuteNonQuery(CommandType.StoredProcedure, CapNhatNgayNghi, sqlParams);


               // this.BulkInsert(ConvertToDataTable(lstnnkb), "HUFS_NGAYNGHI");

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
              
            }
        }
    }
}
