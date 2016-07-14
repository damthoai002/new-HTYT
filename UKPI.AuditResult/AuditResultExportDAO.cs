using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using UKPI.Core;

namespace UKPI.AuditResult
{
    public class AuditResultExportDAO : DataAccess
    {
        private const string SP_EXPORT_AUDIT_RESULT_DT = "p_FPT_ENV_EXPORT_AUDIT_RESULT_DT_SERVICE";
        private const string SP_MARK_SENT_AUDIT_RESULT_DT = "p_FPT_ENV_MARK_SENT_AUDIT_RESULT_SERVICE";
        private const string SP_GET_SALESORG_BY_SHIPTO = "p_FPT_ENV_Get_SalesOrg_ByShipTo";

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(AuditResultExportDAO));

        public AuditResultExportDAO(string connectionString): base(connectionString){}

        public DataTable GetAuditResultForExport()
        {
            try
            {
                DataTable result = this.ExecuteDataTable(CommandType.StoredProcedure, SP_EXPORT_AUDIT_RESULT_DT, null);

                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }

        public string GetSalesOrgByShipTo(string shipToCode)
        {
            try
            {
                SqlParameter[] prs = new SqlParameter[1];
                prs[0] = new SqlParameter("@ShipToCode", shipToCode);

                DataTable result = this.ExecuteDataTable(CommandType.StoredProcedure, SP_GET_SALESORG_BY_SHIPTO, prs);
                
                return (result != null && result.Rows.Count > 0) ? result.Rows[0][0].ToString().Trim() : "V001";
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }

        public void MarkAsSent()
        {
            try
            {
                this.ExecuteNonQuery(CommandType.StoredProcedure, SP_MARK_SENT_AUDIT_RESULT_DT);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}
