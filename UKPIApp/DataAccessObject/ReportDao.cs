
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
    public class ReportDao : clsBaseDAO
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(typeof(ReportDao));
        private const string pInToaThuoc = "intoathuoc";
        private const string pInToaThuocBh = "p_Intoathuocbaohiem";
        private const string BaoCaoThuocTanDuocTTBHYT = "BaoCaoThuocTanDuocTTBHYT";
        private const string BaoCaoThuocYHCTTTBHYT = "BaoCaoThuocYHCTTTBHYT";
        private const string BaoCaoBenhNhanNgoaiTruTTBHYT = "BaoCaoBenhNhanNgoaiTruTTBHYT";
        private const string BaoCaoXuatNhapTon = "BaoCaoXuatNhapTon";
        private const string BaoCaoBenhNhanNamNu = "BaoCaoBenhNhanNamNu";
        private const string SearchBaoCaoLichSuBenhNhan = "SearchBaoCaoLichSuBenhNhan";
        private const string SearchBaoCaoLichSuKhamBenhVaPhatThuoc = "SearchBaoCaoLichSuKhamBenhVaPhatThuoc";
        private const string BaoCaoKhamBenhBHYT_Thang = "BaoCaoKhamBenhBHYT_Thang";
        private const string BaoCaoTongTienBHYT = "BaoCaoTongTienBHYT";
        private const string BaoCaoGhiChuKhac = "BaoCaoGhiChuKhac";
        private const string BaoCaoTheoDoiNghiOm = "BaoCaoTheoDoiNghiOm";
        private const string SearchListBenhNhan = "SearchListBenhNhan";


        public DataTable GetToaThuoc(string maKhamBenh)
        {
            try
            {
                var sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@MaKhamBenh", maKhamBenh);

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, pInToaThuoc, sqlParams);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new DataTable();
            }

        }

        public DataTable GetToaThuocBh(string maKhamBenh)
        {
            try
            {
                var sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@MaKhamBenh", maKhamBenh);

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, pInToaThuocBh, sqlParams);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new DataTable();
            }

        }
        public DataTable baoCaoThuocTanDuocTTBHYT ( string kho, string quy, string nam, string tuNgay, string denNgay)
        {
            try
            {
                var sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@PhongKham", kho);
                sqlParams[1] = new SqlParameter("@Quy", quy);
                sqlParams[2] = new SqlParameter("@Nam", nam);
                sqlParams[3] = new SqlParameter("@TuNgay", tuNgay);
                sqlParams[4] = new SqlParameter("@DenNgay", denNgay);


                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, BaoCaoThuocTanDuocTTBHYT, sqlParams);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new DataTable();
            }

        }
        public DataTable baoCaoThuocYHCTTTBHYT(string kho, string quy, string nam, string tuNgay, string denNgay)
        {
            try
            {
                var sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@PhongKham", kho);
                sqlParams[1] = new SqlParameter("@Quy", quy);
                sqlParams[2] = new SqlParameter("@Nam", nam);
                sqlParams[3] = new SqlParameter("@TuNgay", tuNgay);
                sqlParams[4] = new SqlParameter("@DenNgay", denNgay);

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, BaoCaoThuocYHCTTTBHYT, sqlParams);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new DataTable();
            }

        }

        public DataTable baoCaoBenhNhanNgoaiTruTTBHYT(string kho, string quy, string nam, string tuNgay, string denNgay)
        {
            try
            {

   //              
                var sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@PhongKham", kho);
                sqlParams[1] = new SqlParameter("@Quy", quy);
                sqlParams[2] = new SqlParameter("@Nam", nam);
                sqlParams[3] = new SqlParameter("@TuNgay", tuNgay);
                sqlParams[4] = new SqlParameter("@DenNgay", denNgay);


                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, BaoCaoBenhNhanNgoaiTruTTBHYT, sqlParams);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new DataTable();
            }

        }
      public DataTable baoCaoBenhNhanNamNu(string kho, string quy, string nam, string tuNgay, string denNgay)
        
        {
            try
            {

   //              
                var sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@PhongKham", kho);
                sqlParams[1] = new SqlParameter("@Thang", quy);
                sqlParams[2] = new SqlParameter("@Nam", nam);
                sqlParams[3] = new SqlParameter("@TuNgay", tuNgay);
                sqlParams[4] = new SqlParameter("@DenNgay", denNgay);


                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, BaoCaoBenhNhanNamNu, sqlParams);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new DataTable();
            }

        }

      public DataTable baoCaoTongTienBHYT(string kho, string quy, string nam, string tuNgay, string denNgay)
        
        {
            try
            {          
                var sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@PhongKham", kho);
                sqlParams[1] = new SqlParameter("@Thang", quy);
                sqlParams[2] = new SqlParameter("@Nam", nam);
                sqlParams[3] = new SqlParameter("@TuNgay", tuNgay);
                sqlParams[4] = new SqlParameter("@DenNgay", denNgay);


                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, BaoCaoTongTienBHYT, sqlParams);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new DataTable();
            }

        }

      public DataTable baoCaoGhiChuKhac(string kho, string quy, string nam, string tuNgay, string denNgay)
        
        {
            try
            {          
                var sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@PhongKham", kho);
                sqlParams[1] = new SqlParameter("@Thang", quy);
                sqlParams[2] = new SqlParameter("@Nam", nam);
                sqlParams[3] = new SqlParameter("@TuNgay", tuNgay);
                sqlParams[4] = new SqlParameter("@DenNgay", denNgay);


                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, BaoCaoGhiChuKhac, sqlParams);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new DataTable();
            }

        }

      public DataTable baoCaoTheoDoiNghiOm(string kho, string quy, string nam, string tuNgay, string denNgay)
        
        {
            try
            {          
                var sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@PhongKham", kho);
                sqlParams[1] = new SqlParameter("@Thang", quy);
                sqlParams[2] = new SqlParameter("@Nam", nam);
                sqlParams[3] = new SqlParameter("@TuNgay", tuNgay);
                sqlParams[4] = new SqlParameter("@DenNgay", denNgay);

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, BaoCaoTheoDoiNghiOm, sqlParams);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new DataTable();
            }

        }

        public DataTable baoCaoXuatNhapTon(string kho, string quy, string nam, string tuNgay, string denNgay)
        {
            try
            {

                //              
                var sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@PhongKham", kho);
                sqlParams[1] = new SqlParameter("@Quy", quy);
                sqlParams[2] = new SqlParameter("@Nam", nam);
                sqlParams[3] = new SqlParameter("@TuNgay", tuNgay);
                sqlParams[4] = new SqlParameter("@DenNgay", denNgay);


                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, BaoCaoXuatNhapTon, sqlParams);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new DataTable();
            }

        }


        public DataTable baoCaoLichSuBenhNhan(string maBenhNhan, string tenBenhNhan, string maBHYT, string tuNgay, string denNgay, string khuVuc, string boPhan, string nhomBenh, string maBenh, string tenBenh)
        {
            try
            {

                var sqlParams = new SqlParameter[10];
                sqlParams[0] = new SqlParameter("@MaBenhNhan", maBenhNhan);
                sqlParams[1] = new SqlParameter("@MaBHYT", maBHYT);
                sqlParams[2] = new SqlParameter("@TenBenhNhan", tenBenhNhan);
                sqlParams[3] = new SqlParameter("@KhuVuc", khuVuc);
                sqlParams[4] = new SqlParameter("@BoPhan", boPhan);
                sqlParams[5] = new SqlParameter("@NhomBenh", nhomBenh);
                sqlParams[6] = new SqlParameter("@TuNgay", tuNgay);
                sqlParams[7] = new SqlParameter("@DenNgay", denNgay);
                sqlParams[8] = new SqlParameter("@MaBenh", maBenh);
                sqlParams[9] = new SqlParameter("@TenBenh", tenBenh);

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, SearchBaoCaoLichSuBenhNhan, sqlParams);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new DataTable();
            }

        }
      
           public DataTable ListBenhNhan(string maBenhNhan, string tenBenhNhan, string maBHYT, string tuNgay, string denNgay, string khuVuc, string boPhan, string nhomBenh, string maBenh, string tenBenh)
        {
            try
            {

                var sqlParams = new SqlParameter[10];
                sqlParams[0] = new SqlParameter("@MaBenhNhan", maBenhNhan);
                sqlParams[1] = new SqlParameter("@MaBHYT", maBHYT);
                sqlParams[2] = new SqlParameter("@TenBenhNhan", tenBenhNhan);
                sqlParams[3] = new SqlParameter("@KhuVuc", khuVuc);
                sqlParams[4] = new SqlParameter("@BoPhan", boPhan);
                sqlParams[5] = new SqlParameter("@NhomBenh", nhomBenh);
                sqlParams[6] = new SqlParameter("@TuNgay", tuNgay);
                sqlParams[7] = new SqlParameter("@DenNgay", denNgay);
                sqlParams[8] = new SqlParameter("@MaBenh", maBenh);
                sqlParams[9] = new SqlParameter("@TenBenh", tenBenh);

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, SearchListBenhNhan, sqlParams);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new DataTable();
            }

        }
      
        
        
        public DataTable baoCaoLichSuKhamBenhVaPhatThuoc(string maBenhNhan, string tenBenhNhan, string maBHYT, string tuNgay, string denNgay, string khuVuc, string boPhan, string nhomBenh, string maBenh, string tenBenh)
        {
            try
            {

                var sqlParams = new SqlParameter[10];
                sqlParams[0] = new SqlParameter("@MaBenhNhan", maBenhNhan);
                sqlParams[1] = new SqlParameter("@MaBHYT", maBHYT);
                sqlParams[2] = new SqlParameter("@TenBenhNhan", tenBenhNhan);
                sqlParams[3] = new SqlParameter("@KhuVuc", khuVuc);
                sqlParams[4] = new SqlParameter("@BoPhan", boPhan);
                sqlParams[5] = new SqlParameter("@NhomBenh", nhomBenh);
                sqlParams[6] = new SqlParameter("@TuNgay", tuNgay);
                sqlParams[7] = new SqlParameter("@DenNgay", denNgay);
                sqlParams[8] = new SqlParameter("@MaBenh", maBenh);
                sqlParams[9] = new SqlParameter("@TenBenh", tenBenh);

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, SearchBaoCaoLichSuKhamBenhVaPhatThuoc, sqlParams);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new DataTable();
            }

        }
        public DataTable baoCaoKhamBenhBHYT_Thang(string kho, string nam, string thang)
        {
            try
            {

                //              
                var sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@PhongKham", kho);
                sqlParams[1] = new SqlParameter("@Thang", thang);
                sqlParams[2] = new SqlParameter("@Nam", nam);


                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, BaoCaoKhamBenhBHYT_Thang, sqlParams);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new DataTable();
            }
        }

    }
}
