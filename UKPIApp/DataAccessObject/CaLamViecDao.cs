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
    public class CaLamViecDao : clsBaseDAO
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(CaLamViecDao));

        private const string PGetCaLamViec = "p_GetCaLamViec";
        private const string PGetDauDocThe = "p_GetDauDocThe";
        public DataTable GetCalamViec()
        {

            try
            {
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetCaLamViec);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public DataTable GetDauDocThe()
        {

            try
            {
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetDauDocThe);

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
