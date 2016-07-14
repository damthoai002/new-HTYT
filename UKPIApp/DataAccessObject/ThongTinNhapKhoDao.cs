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
    public class ThongTinNhapKhoDao : clsBaseDAO
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(ThongTinNhapKhoDao));
        private const string p_HUFS_GetMaxMaNhapKho = "p_HUFS_GetMaxMaNhapKho";
        private const string p_HUFS_SearchXuatKho = "p_HUFS_SearchXuatKho";
        private const string p_HUFS_insertDataForTransactionKhiNhapKho = "p_HUFS_insertDataForTransactionKhiNhapKho";
        private const string p_HUFS_UpdateThongTinGiaThuocKhiNhapKho = "p_HUFS_UpdateThongTinGiaThuocKhiNhapKho";
        private const string p_HUFS_ProcessBackupSoLuongThuocKhiNhapKho = "p_HUFS_ProcessBackupSoLuongThuocKhiNhapKho";
        
        public string GetMaxMaNhapKho()
        {

            try
            {
                string maxMaKhamBenh = "";
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_GetMaxMaNhapKho);
                foreach (DataRow dr in dtResult.Rows)
                {
                    maxMaKhamBenh = dr["MaxMaNhapKho"].ToString();
                    break;
                }
                return maxMaKhamBenh;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return "";
            }
        }
        public string GenerateNewMaNhapKho()
        {
            string currentMaxMaNhapKho = GetMaxMaNhapKho();
            //MKB000000000040
            int lenght = currentMaxMaNhapKho.Length;
            int prefixLenght = KeyPrefix.MaNhapKho.Length;

            string currentNumber = !string.IsNullOrEmpty(currentMaxMaNhapKho) ? currentMaxMaNhapKho.Remove(0, prefixLenght): "0";
            long keyNumber = 0;
            try
            {
                keyNumber = long.Parse(currentNumber);
            }
            catch
            {

            }
            keyNumber += 1;

            int newNumberLenght = keyNumber.ToString().Length;
            string paddingKey = "";
            for (int i = 0; i < lenght - (prefixLenght + newNumberLenght); i++)
            {
                paddingKey += "0";
            }

            string newKey = "";
            newKey = KeyPrefix.MaNhapKho + paddingKey + keyNumber.ToString();
            return newKey;
        }
        public DataTable LoadThongTinXuatKho(string maThuoc)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@maThuoc", maThuoc);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_SearchXuatKho, Params);
                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }
        private void ProcessBackupSoLuongThuocKhiNhapKho(string maNhapKho)
        {
             try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@MaNhapKho", maNhapKho);
                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_ProcessBackupSoLuongThuocKhiNhapKho, Params);
   
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }

        }
        public bool UpdateThongTinGiaThuocKhiNhapKho(string maNhapKho)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@maNhapKho", maNhapKho);
                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_UpdateThongTinGiaThuocKhiNhapKho, Params);
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
        }
        public bool SaveThongTinNhapKho(ThongTinNhapKho thongTinNhapKho,List<ThongTinNhapKhoDetail> listThongTinNhapKhoDetail)
        {
            string conStr = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    if (thongTinNhapKho == null)
                        return false;
                    else
                    {
                        //create temporay list for bulk insert
                        List<ThongTinNhapKho> listThongTinNhapKho = new List<ThongTinNhapKho>();
                        listThongTinNhapKho.Add(thongTinNhapKho);
                        this.BulkInsert(ConvertToDataTable(listThongTinNhapKho), "HUFS_NHAPKHO_HEADER");
                        if (listThongTinNhapKhoDetail != null && listThongTinNhapKhoDetail.Count > 0)
                        {
                            this.BulkInsert(ConvertToDataTable(listThongTinNhapKhoDetail), "HUFS_NHAPKHO_DETAIL");
                            UpdateThongTinGiaThuocKhiNhapKho(thongTinNhapKho.MaNhapKho);
                            ProcessBackupSoLuongThuocKhiNhapKho(thongTinNhapKho.MaNhapKho);
                            for (int i = 0; i < listThongTinNhapKhoDetail.Count; i++)
                            {
                                bool result = InsertThongTinGiaoDichKhiNhapKho(listThongTinNhapKhoDetail[i].MaThuoc,
                                                                     listThongTinNhapKhoDetail[i].SoLuong,
                                                                     listThongTinNhapKhoDetail[i].LoThuoc,
                                                                     listThongTinNhapKhoDetail[i].HanSuDung);
                                if (!result)
                                {
                                    transaction.Rollback();
                                    return false;
                                }
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    log.Error(ex.Message, ex);
                    return false;
                }
                finally
                {
                    con.Close();
                }
            }
            return true;
        }
        private bool InsertThongTinGiaoDichKhiNhapKho(string maThuoc, long soLuong, string maLoThuoc,DateTime hanSuDung)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[4];
                Params[0] = new SqlParameter("@MaThuoc", maThuoc);
                Params[1] = new SqlParameter("@Soluong", soLuong);
                Params[2] = new SqlParameter("@MaLoThuoc", maLoThuoc);
                Params[3] = new SqlParameter("@HanSuDung", hanSuDung);
                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_insertDataForTransactionKhiNhapKho, Params);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
