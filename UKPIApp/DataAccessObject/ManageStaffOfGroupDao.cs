using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using log4net;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.DataAccessObject
{
    public class ManageStaffOfGroupDao : clsBaseDAO
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(ManageStaffOfGroupDao));



        //Constant Store Proceduce


      //  public const string PGetNhomByTruongNhom  = "p_Get_NhomByTruongNhom";
        public const string PSearchNhanVienByName = "p_SearchNhanVienByName";

        public const string PGetNhanVienCC = "p_GetNhanVienCC";

        public const string PSearchNhanVienCC = "p_SearchNhanVienCC";

        public const string PGetThongTinNhom = "p_GetThongTinNhom";
        public const string PGetThanhVienNhom = "p_GetThanhVienNhom";

        public const string PAddNvToGroup = "p_AddNvToGroup";
        public const string PRemoveNvToGroup = "p_RemoveNvToGroup";
        public DataTable GetThanhVienNhom(string nhom)
        {
            try
            {
                var sqlParas = new SqlParameter[1];
                sqlParas[0] = new SqlParameter("@Nhom", nhom);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetThanhVienNhom, sqlParas);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }


        public DataTable GetNhomByNhomTruong(string truongNhomId)
        {
            try
            {
                var sqlParas = new SqlParameter[1];
                sqlParas[0] = new SqlParameter("@TruongNhom", truongNhomId);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetThongTinNhom, sqlParas);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }


        public DataTable SearchTvNhom(int nhomId, int truongNhomId, string tenNhanVien)
        {
            try
            {
                var sqlParas = new SqlParameter[3];
                sqlParas[0] = new SqlParameter("@Nhom", nhomId);
                sqlParas[1] = new SqlParameter("@TruongNhomId", truongNhomId);
                sqlParas[2] = new SqlParameter("@TenNhanVien", tenNhanVien);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PSearchNhanVienByName, sqlParas);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }


        public DataTable GetNvCc()
        {
            try
            {

                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNhanVienCC);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }


        public DataTable SearchNvCc( string ten, string ho,  string loaiNv, string maThe, string maNvUnilever)
        {
            try
            {
                var sqlParas = new SqlParameter[5];
                sqlParas[0] = new SqlParameter("@Ten", ten);
                sqlParas[1] = new SqlParameter("@Ho", ho);
                sqlParas[2] = new SqlParameter("@LoaiNV", loaiNv);
                sqlParas[3] = new SqlParameter("@MaThe", maThe);
                sqlParas[4] = new SqlParameter("@MaNvUnilever", maNvUnilever);

                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PSearchNhanVienCC, sqlParas);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }


        public void AppNvToGroup(string sysId, string maNhom, string maTruongNhom)
        {
            AppNvToGroup(sysId, maNhom, maTruongNhom, null);

        }

        public void AppNvToGroup(string sysId,string maNhom,string maTruongNhom, SqlTransaction trans)
        {

            try
            {        

                var sqlParas = new SqlParameter[3];
                sqlParas[0] = new SqlParameter("@SysId", sysId);
                sqlParas[1] = new SqlParameter("@MaNhom", maNhom);
                sqlParas[2] = new SqlParameter("@MaTruongNhom", maTruongNhom);

                DataServices.ExecuteNonQuery(CommandType.StoredProcedure, PAddNvToGroup, sqlParas);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


   public void RemoveNvToGroup(string sysId, string maNhom, string maTruongNhom)
        {
            RemoveNvToGroup(sysId, maNhom, maTruongNhom, null);

        }

   public void RemoveNvToGroup(string sysId, string maNhom, string maTruongNhom, SqlTransaction trans)
        {

            try
            {        

                var sqlParas = new SqlParameter[3];
                sqlParas[0] = new SqlParameter("@SysId", sysId);
                sqlParas[1] = new SqlParameter("@MaNhom", maNhom);
                sqlParas[2] = new SqlParameter("@MaTruongNhom", maTruongNhom);

                DataServices.ExecuteNonQuery(CommandType.StoredProcedure, PRemoveNvToGroup, sqlParas);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }



    }
}
