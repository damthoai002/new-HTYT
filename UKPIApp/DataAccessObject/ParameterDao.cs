using Excel;
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
    public class ParameterDao : clsBaseDAO
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(ParameterDao));

        private static readonly string SpGetParamByName = "p_Select_Param";


        public static System.Data.DataTable SelectParamByName(string paramName)
        {

            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ParamName", paramName);

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, SpGetParamByName, param);
            }
            catch (Exception ex)
            {

                log.Error(ex.Message, ex);
                throw ex;
            }


        }


    }
}
