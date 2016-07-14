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
    public class ChotTonKhoDao : clsBaseDAO
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(ChotTonKhoDao));
        private const string p_HUFS_ProcessChotTonKhoHeader = "p_HUFS_ProcessChotTonKhoHeader";
        private const string p_HUFS_ProcessChotTonKhoDetailTinhTonKho = "p_HUFS_ProcessChotTonKhoDetailTinhTonKho";
        private const string p_HUFS_ProcessChotTonKhoDetailXacNhan = "p_HUFS_ProcessChotTonKhoDetailXacNhan";
        private const string p_HUFS_ProcessChotTonKhoHeaderWorkflow = "p_HUFS_ProcessChotTonKhoHeaderWorkflow";
        private const string p_HUFS_ProcessChotTonKhoDetailChotTon = "p_HUFS_ProcessChotTonKhoDetailChotTon";
        private const string p_HUFS_ProcessChotTonKhoHeaderStatus = "p_HUFS_ProcessChotTonKhoHeaderStatus";
        private const string p_HUFS_SearchChotTonKhoHeader = "p_HUFS_SearchChotTonKhoHeader";
        private const string p_HUFS_LoadChotTonKhoDetail = "p_HUFS_LoadChotTonKhoDetail";
        private const string p_HUFS_CheckThongTinChotTon = "p_HUFS_CheckThongTinChotTon";
        private const int LuuChotTon = 0;
        private const int TinhChotTon = 1;
        private const int XacNhanChotTon = 2;
        private const int ChotTon = 3;

        public int CheckChotTonDangHoatDong(string maKho)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@MaKho", maKho);
                int soLuong = -1;
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_CheckThongTinChotTon, Params);
                foreach (DataRow dr in dtResult.Rows)
                {
                    soLuong = int.Parse(dr["Result"].ToString());
                    break;
                }
                return soLuong;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return -1;
            }
        }
        public List<ChotTonKhoHeader> SearchChotTonKho(string maChotTonKho,string dienGiai, string tenKho,DateTime ngayTaoPhieu,string status,bool isUseDate)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[6];
                Params[0] = new SqlParameter("@MaChotTonKho", maChotTonKho);
                Params[1] = new SqlParameter("@DienGiai", dienGiai);
                Params[2] = new SqlParameter("@TenKho", tenKho);
                Params[3] = new SqlParameter("@NgayTaoPhieu", ngayTaoPhieu);
                Params[4] = new SqlParameter("@Status", status);
                Params[5] = new SqlParameter("@IsUseDate", isUseDate);

                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_SearchChotTonKhoHeader, Params);
                return this.ConvertDataTableToList<ChotTonKhoHeader>(dtResult);
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                return null;
            }
        }

        public List<ChotTonKhoDetail> LoadChotTonKhoDetail(string maChotTonKho)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@MaChotTonKho", maChotTonKho);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_LoadChotTonKhoDetail, Params);
                return this.ConvertDataTableToList<ChotTonKhoDetail>(dtResult);
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                return null;
            }
        }
        public List<ChotTonKhoDetail> ProcessChotTonKhoDetailTinhTonKho(string maChotTonKho,string tenKho)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@MaChotTonKho", maChotTonKho);
                Params[1] = new SqlParameter("@MaKho", tenKho);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_ProcessChotTonKhoDetailTinhTonKho, Params);
                return this.ConvertDataTableToList<ChotTonKhoDetail>(dtResult);
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                return null;
            }
        }
        private void ProcessChotTonKhoHeaderWorkflow(string maCHotTonKho,int currentWorklow)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@MaChotTonKho", maCHotTonKho);
                Params[1] = new SqlParameter("@CurrentWorkflow", currentWorklow);
                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_ProcessChotTonKhoHeaderWorkflow, Params);
            }catch(Exception ex)
            {
                log.Info(ex.Message);
            }
        }

        private void ProcessDongChotTonKho(string maCHotTonKho, string trangThai)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@MaChotTonKho", maCHotTonKho);
                Params[1] = new SqlParameter("@Status", trangThai);
                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_ProcessChotTonKhoHeaderStatus, Params);
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
            }
        }
        public bool ProcessChotTonKhoDetailXacNhan(List<ChotTonKhoDetail> list,string maChotTonKho)
        {
            try
            {
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count;i++ )
                    {
                        SqlParameter[] Params = new SqlParameter[4];
                        Params[0] = new SqlParameter("@Id", list[i].Id);
                        Params[1] = new SqlParameter("@SoLuongThucTe", list[i].SoLuongThucTe);
                        Params[2] = new SqlParameter("@SoLuongChenhLech", list[i].SoLuongChenhLech);
                        Params[3] = new SqlParameter("@LoaiChenhLech", list[i].LoaiChenhLech);
                        DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_ProcessChotTonKhoDetailXacNhan, Params);
                    }
                    ProcessChotTonKhoHeaderWorkflow(maChotTonKho, XacNhanChotTon);
                }
                return true;
            }catch(Exception ex)
            {
                log.Info(ex.Message);
                return false;
            }
        }

        public bool ProcessChotTonKhoDetailChotTon(List<ChotTonKhoDetail> list, string maChotTonKho)
        {
            try
            {
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        SqlParameter[] Params = new SqlParameter[6];
                        Params[0] = new SqlParameter("@Id", list[i].Id);
                        Params[1] = new SqlParameter("@SoLuongTon", list[i].SoLuongTon);
                        Params[2] = new SqlParameter("@SoLuongThucTe", list[i].SoLuongThucTe);
                        Params[3] = new SqlParameter("@SoLuongChenhLech", list[i].SoLuongChenhLech);
                        Params[4] = new SqlParameter("@LoaiChenhLech", list[i].LoaiChenhLech);
                        Params[5] = new SqlParameter("@MaNhapKhoDetail", list[i].MaNhapKhoDetail);
                        DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_ProcessChotTonKhoDetailChotTon, Params);
                    }
                    ProcessChotTonKhoHeaderWorkflow(maChotTonKho, ChotTon);
                    ProcessDongChotTonKho(maChotTonKho, "Đóng");
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                return false;
            }
        }
        public bool ProcessChotTonKhoHeader(ChotTonKhoHeader ctkh)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[12];
                Params[0] = new SqlParameter("@MaChotTonKho", ctkh.MaChotTonKho);
                Params[1] = new SqlParameter("@DienGiai", ctkh.DienGiai);
                Params[2] = new SqlParameter("@TenKho", ctkh.TenKho);
                Params[3] = new SqlParameter("@NgayTaoPhieu", ctkh.NgayTaoPhieu);
                Params[4] = new SqlParameter("@NguoiXacNhan", ctkh.NguoiXacNhan);
                Params[5] = new SqlParameter("@NguoiDieuChinh", ctkh.NguoiDieuChinh);
                Params[6] = new SqlParameter("@CreatedDate", ctkh.CreatedDate);
                Params[7] = new SqlParameter("@ModifiedDate", ctkh.ModifiedDate);
                Params[8] = new SqlParameter("@Creator", ctkh.Creator);
                Params[9] = new SqlParameter("@LastModifier", ctkh.LastModifier);
                Params[10] = new SqlParameter("@Status", ctkh.Status);
                Params[11] = new SqlParameter("@IsDeleted", ctkh.IsDeleted);
                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_ProcessChotTonKhoHeader, Params);
                return true;
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                return false;
            }
        }

    }
}
