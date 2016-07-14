
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
    public class TeamDao : clsBaseDAO
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(typeof(TeamDao));

        private const string SpGetAllTeam = "p_GetAllTeam";
        private const string SpUnActiveTeam = "p_UnActiveTeam";
        private const string SpUnActiveNhanVienQuanLyNhom = "p_UnActiveNhanVienQuanLyNhom";
        private const string SpSearchTeam = "p_SearchTeam";
        private const string SpCreateTeam = "p_CreateTeam";

        private const string SpGetTruongNhomForTeam = "p_GetTruongNhomForTeam";

        public System.Data.DataTable GetAllTeams()
        {

            try
            {

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, SpGetAllTeam);
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message, ex);
                throw ex;
            }


        }
        public System.Data.DataTable GetTruongNhomForTeam()
        {
            try
            {
                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, SpGetTruongNhomForTeam);
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message, ex);
                throw ex;
            }


        }
        public DataTable SearchTeam(string ten, string ho, string nhom)
        {
            try
            {
                var sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@Ten", ten);
                sqlParams[1] = new SqlParameter("@Ho", ho);
                sqlParams[2] = new SqlParameter("@Nhom", nhom);
                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, SpSearchTeam, sqlParams);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }

        }


        public void DoUnActiveTeam(List<ClsTeam> teams, string strTeamId, string userid)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = GetConnection(clsCommon.GetConnectionString());
                //Open connection
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                trans = conn.BeginTransaction();


                UnActiveTeam(strTeamId, userid, trans);

                foreach (var t in teams)
                {
                    UnActiveNhanVienQuanLyNhom(t.UserName, t.NhomId, userid, trans);
                }


                trans.Commit();//Commit
            }
            catch (System.Exception ex)
            {
                Log.Error(ex.ToString());
                if (trans != null)
                    trans.Rollback();   //Rollback data
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();   //Close connection
            }


        }


        public void UnActiveTeam(string sysId, string userId)
        {

            UnActiveTeam(sysId, userId, null);
        }
        public void UnActiveTeam(string sysId, string userId, SqlTransaction trans)
        {
            try
            {
                var sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@Nhom", sysId);
                sqlParams[1] = new SqlParameter("@UserId", userId);
                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, SpUnActiveTeam, sqlParams);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }




        public void UnActiveNhanVienQuanLyNhom(string truongNhomId, string nhomId, string userId)
        {
            UnActiveNhanVienQuanLyNhom(nhomId, truongNhomId, userId, null);

        }
        public void UnActiveNhanVienQuanLyNhom(string truongNhomId, string nhomId, string userId, SqlTransaction trans)
        {

            try
            {
                var sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@Nhom", nhomId);
                sqlParams[1] = new SqlParameter("@USERNAME", truongNhomId);
                sqlParams[2] = new SqlParameter("@UserId", userId);
                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, SpUnActiveNhanVienQuanLyNhom, sqlParams);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void CreateTeam(ClsTeam team, string username)
        {

            try
            {
                var sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@TenNhom", team.TenNhom);
                sqlParams[1] = new SqlParameter("@LoaiNhom", team.LoaiNhom);
                sqlParams[2] = new SqlParameter("@IsOt", team.IsOt);
                sqlParams[3] = new SqlParameter("@IsOutsource", team.IsOutsource);
                sqlParams[4] = new SqlParameter("@MoTa", team.MoTa);
                sqlParams[5] = new SqlParameter("@Is_Active", team.IsActive);
                sqlParams[6] = new SqlParameter("@UserName", team.UserName);
                sqlParams[7] = new SqlParameter("@UserId", username);



                DataServices.ExecuteNonQuery(CommandType.StoredProcedure, SpCreateTeam, sqlParams);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }


    }
}
