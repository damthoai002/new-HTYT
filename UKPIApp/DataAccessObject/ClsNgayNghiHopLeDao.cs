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
    public class ClsNgayNghiHopLeDao : clsBaseDAO
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(ClsNgayNghiHopLeDao));

        private const string PSearchNhanVienNghiPhep = "p_SearchNhanVienNghiPhep";
        private const string PSearchNhanVienNghiThaiSan = "p_SearchNhanVienNghiThaiSan";


        private const string PTaoNgayNghiPhep = "p_TaoNgayNghiPhep";
        private const string PTaoNgayNghiTrongNam = "p_TaoNgayNghiTrongNam";

        private const string PGetNgayNghiPhepInChamCong = "p_GetNgayNghiPhepInChamCong";
        private const string PUpdateIsOffForNgayNghi = "p_UpdateIsOffForNgayNghi";

        private const string PGetEmailNhanVien = "p_GetEmailNhanVien";
        private const string PCheckExistDateOff = "p_CheckExistDateOff";

        private const string PGetNgayNghiPhep = "p_GetNgayNghiPhep";

        private const string PTaoNgayNghiTaiNan = "p_TaoNgayNghiTaiNan";
        private const string PGetNgayNghiTaiNan = "p_GetNgayNghiTaiNan";

        private const string PTaoNgayNghiThaiSan = "p_TaoNgayNghiThaiSan";
        private const string PGetNgayNghiThaiSan = "p_GetNgayNghiThaiSan";


        public DataTable GetNhanVienNghiPhep(string truongnhomId)
        {

            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@TruongNhom", truongnhomId);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PSearchNhanVienNghiPhep, Params);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public DataTable GetNhanVienNghiThaiSan(string truongNhomId)
        {

            try
            {

                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@TruongNhom", truongNhomId);

                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PSearchNhanVienNghiThaiSan, Params);

                return dtResult;
            }
            catch (Exception ex)
            {

                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        public void TaoNgayNghiPhep(
                    string truongNhom,
                    string maGiaoDich,
                    DateTime tuNgay,
                    DateTime denNgay,
                    string lyDo1,
                    string lyDo1ChiTiet,
                    string lyDo2,
                    string lyDo2ChiTiet,
                    string dienGiai,
                    int maNvNghi,
                    string tenNvNghi,
                    string tenNvDuyet,
                    string ngayDuyet,
                    bool status
           )
        {
            try
            {


                SqlParameter[] Params = new SqlParameter[14];
                Params[0] = new SqlParameter("@TruongNhom", truongNhom);
                Params[1] = new SqlParameter("@MaGiaoDich", maGiaoDich);
                Params[2] = new SqlParameter("@TuNgay", tuNgay);
                Params[3] = new SqlParameter("@DenNgay", denNgay);
                Params[4] = new SqlParameter("@LyDo1", lyDo1);
                Params[5] = new SqlParameter("@LyDo1ChiTiet", lyDo1ChiTiet);
                Params[6] = new SqlParameter("@LyDo2", lyDo2);
                Params[7] = new SqlParameter("@LyDo2ChiTiet", lyDo2ChiTiet);
                Params[8] = new SqlParameter("@DienGiai", dienGiai);
                Params[9] = new SqlParameter("@MaNVNghi", maNvNghi);
                Params[10] = new SqlParameter("@TenNVNghi", tenNvNghi);
                Params[11] = new SqlParameter("@TenNVDuyet", tenNvDuyet);
                Params[12] = new SqlParameter("@NgayDuyet", ngayDuyet);
                Params[13] = new SqlParameter("@Status", status);



                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, PTaoNgayNghiPhep, Params);



            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }





        public DataTable CheckApprovedForNgayNghi(string maNv, string maTruongNhom, string maGiaoDich)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[3];
                Params[0] = new SqlParameter("@MaTruongNhom", maTruongNhom);
                Params[1] = new SqlParameter("@MaNV", maNv);
                Params[2] = new SqlParameter("@MaGiaoDich", maGiaoDich);

                DataTable dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNgayNghiPhepInChamCong, Params);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }
        public void UpdateIsOffForNgayNghiNhanVien(string maNv, string maTruongNhom, string maGiaoDich)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[3];
                Params[0] = new SqlParameter("@MaTruongNhom", maTruongNhom);
                Params[1] = new SqlParameter("@MaNV", maNv);
                Params[2] = new SqlParameter("@MaGiaoDich", maGiaoDich);

                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, PUpdateIsOffForNgayNghi, Params);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public DataTable GetEmailNhanVien(string maNhanVien, string maTruongNhom)
        {

            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@MaTruongNhom", maTruongNhom);
                Params[1] = new SqlParameter("@MaNV", maNhanVien);


                DataTable dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetEmailNhanVien, Params);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        public DataTable CheckExistDateOff(string fromDate, string toDate, string maTruongNhom, string maNv)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[4];
                Params[0] = new SqlParameter("@MaTruongNhom", maTruongNhom);
                Params[1] = new SqlParameter("@MaNV", maNv);
                Params[2] = new SqlParameter("@FromDate", fromDate);
                Params[3] = new SqlParameter("@ToDate", toDate);



                DataTable dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PCheckExistDateOff, Params);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }


        }

        public DataTable GetNgaynghiPhep(string maTruongNhom, string tenNhanVien)
        {


            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@MaTruongNhom", maTruongNhom);
                Params[1] = new SqlParameter("@TenNhanVien", tenNhanVien);



                DataTable dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNgayNghiPhep, Params);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }

        public void TaoNgayNghiTaiNan(
                  string truongNhom,
                  string maGiaoDich,
                  DateTime tuNgay,
                  DateTime denNgay,
                  string lyDo1,
                  string lyDo1ChiTiet,
                  string lyDo2,
                  string lyDo2ChiTiet,
                  string dienGiai,
                  int maNvNghi,
                  string tenNvNghi,
                  string tenNvDuyet,
                  string ngayDuyet,
                  bool status
         )
        {
            try
            {


                SqlParameter[] Params = new SqlParameter[12];
                Params[0] = new SqlParameter("@TruongNhom", truongNhom);
                Params[1] = new SqlParameter("@MaGiaoDich", maGiaoDich);
                Params[2] = new SqlParameter("@TuNgay", tuNgay);
                Params[3] = new SqlParameter("@DenNgay", denNgay);
                Params[4] = new SqlParameter("@LyDo1", lyDo1);
                Params[5] = new SqlParameter("@LyDo1ChiTiet", lyDo1ChiTiet);
                //Params[6] = new SqlParameter("@LyDo2", lyDo2);
                //Params[7] = new SqlParameter("@LyDo2ChiTiet", lyDo2ChiTiet);
                Params[6] = new SqlParameter("@DienGiai", dienGiai);
                Params[7] = new SqlParameter("@MaNVNghi", maNvNghi);
                Params[8] = new SqlParameter("@TenNVNghi", tenNvNghi);
                Params[9] = new SqlParameter("@TenNVDuyet", tenNvDuyet);
                Params[10] = new SqlParameter("@NgayDuyet", ngayDuyet);
                Params[11] = new SqlParameter("@Status", status);



                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, PTaoNgayNghiTaiNan, Params);



            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }

        public DataTable GetNgaynghiTaiNan(string maTruongNhom, string tenNhanVien)
        {


            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@MaTruongNhom", maTruongNhom);
                Params[1] = new SqlParameter("@TenNhanVien", tenNhanVien);



                DataTable dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNgayNghiTaiNan, Params);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }


        public void TaoNgayNghiThaiSan(
                  string truongNhom,
                  string maGiaoDich,
                  DateTime tuNgay,
                  DateTime denNgay,
                  string lyDo1,
                  string lyDo1ChiTiet,
                  string lyDo2,
                  string lyDo2ChiTiet,
                  string dienGiai,
                  int maNvNghi,
                  string tenNvNghi,
                  string tenNvDuyet,
                  string ngayDuyet,
                  bool status
         )
        {
            try
            {


                SqlParameter[] Params = new SqlParameter[14];
                Params[0] = new SqlParameter("@TruongNhom", truongNhom);
                Params[1] = new SqlParameter("@MaGiaoDich", maGiaoDich);
                Params[2] = new SqlParameter("@TuNgay", tuNgay);
                Params[3] = new SqlParameter("@DenNgay", denNgay);
                Params[4] = new SqlParameter("@LyDo1", lyDo1);
                Params[5] = new SqlParameter("@LyDo1ChiTiet", lyDo1ChiTiet);
                Params[6] = new SqlParameter("@LyDo2", lyDo2);
                Params[7] = new SqlParameter("@LyDo2ChiTiet", lyDo2ChiTiet);
                Params[8] = new SqlParameter("@DienGiai", dienGiai);
                Params[9] = new SqlParameter("@MaNVNghi", maNvNghi);
                Params[10] = new SqlParameter("@TenNVNghi", tenNvNghi);
                Params[11] = new SqlParameter("@TenNVDuyet", tenNvDuyet);
                Params[12] = new SqlParameter("@NgayDuyet", ngayDuyet);
                Params[13] = new SqlParameter("@Status", status);



                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, PTaoNgayNghiThaiSan, Params);



            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }

        public DataTable GetNgaynghiThaiSan(string maTruongNhom, string tenNhanVien, string ngayNghi)
        {


            try
            {
                SqlParameter[] Params = new SqlParameter[3];
                Params[0] = new SqlParameter("@MaTruongNhom", maTruongNhom);
                Params[1] = new SqlParameter("@TenNhanVien", tenNhanVien);
                Params[2] = new SqlParameter("@NgayNghi", ngayNghi);


                DataTable dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNgayNghiThaiSan, Params);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }

    }
}
