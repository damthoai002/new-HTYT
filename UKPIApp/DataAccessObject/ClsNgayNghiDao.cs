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
    public class ClsNgayNghiDao : clsBaseDAO
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(ClsNgayNghiDao));

        private const string PSearchNgayNghi = "p_SearchNgayNghi";
        private const string PTaoNgayNghiChuNhat = "p_TaoNgayNghiChuNhat";
        private const string PTaoNgayNghiTrongNam = "p_TaoNgayNghiTrongNam";
        private const string PNgungSuDungNgayNghi = "p_NgungSuDungNgayNghi";

        private const string PTaoNgayNghiThu7 = "p_TaoNgayNghiThu7";


        public DataTable GetNgayNghi(int nam)
        {
           
            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@Nam", nam);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PSearchNgayNghi, Params);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void TaoNgayNghiChuNhat(string truongNhomId, int year, string moTa )
        {

            try
            {

                SqlParameter[] Params = new SqlParameter[3];
                Params[0] = new SqlParameter("@TruongNhomId", truongNhomId);
                Params[1] = new SqlParameter("@Year", year);
                Params[2] = new SqlParameter("@MoTa", moTa);

                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, PTaoNgayNghiChuNhat, Params);

            }
            catch (Exception ex)
            {
               
                log.Error(ex.Message, ex);
                throw ex;
            }
        }



        public void TaoNgayNghiThu7(string truongNhomId, int year, string moTa)
        {

            try
            {

                SqlParameter[] Params = new SqlParameter[3];
                Params[0] = new SqlParameter("@TruongNhomId", truongNhomId);
                Params[1] = new SqlParameter("@Year", year);
                Params[2] = new SqlParameter("@MoTa", moTa);

                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, PTaoNgayNghiThu7, Params);

            }
            catch (Exception ex)
            {

                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void TaoNgayNghiTrongNam(string maNgayNghi, DateTime ngayBatDau, DateTime ngayKetThuc, string mota, string createId)
        {
            try
            {

                SqlParameter[] Params = new SqlParameter[5];
                Params[0] = new SqlParameter("@MaNgayNghi", maNgayNghi);
                Params[1] = new SqlParameter("@NgayBatDau", ngayBatDau);
                Params[2] = new SqlParameter("@NgayKetThuc", ngayKetThuc);
                Params[3] = new SqlParameter("@MoTa", mota);
                Params[4] = new SqlParameter("@CreaterId", createId);


                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, PTaoNgayNghiTrongNam, Params);



            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }
        public void NgungSuDungNgayNghi(string sysId)
        {
            try
            {

                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@SysId", sysId);
                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, PNgungSuDungNgayNghi, Params);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }
    }
}
