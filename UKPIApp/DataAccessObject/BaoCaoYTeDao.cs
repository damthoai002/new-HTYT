using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using UKPI.Utils;

namespace UKPI.DataAccessObject
{
    public class BaoCaoYTeDao : clsBaseDAO
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(BaoCaoYTeDao));
        private const string p_HUFS_SearchLichSuBenhNhanNew = "p_HUFS_SearchLichSuBenhNhanNew";
        private const string p_HUFS_SearchLichSuKho = "p_HUFS_SearchLichSuKhoNew ";
        public DataTable LoadThongTinLichSuBenhNhan(string maBenhNhan, string maBHYT, string tenBenhNhan)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[3];
                Params[0] = new SqlParameter("@MaBenhNhan", maBenhNhan);
                Params[1] = new SqlParameter("@MaBHYT", maBHYT);
                Params[2] = new SqlParameter("@TenBenhNhan", tenBenhNhan);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_SearchLichSuBenhNhanNew, Params);
                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }

        public DataTable LoadThongTinLichSuKho(string kho, string loaiThuoc)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@Kho", kho);
                Params[1] = new SqlParameter("@LoaiThuoc", loaiThuoc);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_SearchLichSuKho, Params);
                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }
    }
}
