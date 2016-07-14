using FPT.Component.ExcelPlus;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UKPI.DataAccessObject;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.BusinessObject
{
    public class ParameterBo : clsBaseBO
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(ParameterBo));

        public static ClsParameter GetParamByName(string paramName)
        {
            try
            {
                var objParam = new ClsParameter();
                var table = ParameterDao.SelectParamByName(paramName);
                if (table.Rows.Count > 0)
                {
                    objParam.ParamName = table.Rows[0][clsCommon.Parameter.ParamName].ToString();
                    objParam.ParamType = table.Rows[0][clsCommon.Parameter.ParamType].ToString();
                    objParam.ParamValue = table.Rows[0][clsCommon.Parameter.ParamValue].ToString();
                    objParam.ParamGroup = table.Rows[0][clsCommon.Parameter.ParamGroup].ToString();
                    objParam.Status = Int16.Parse(table.Rows[0][clsCommon.Parameter.Status].ToString());
                    objParam.Description = table.Rows[0][clsCommon.Parameter.Description].ToString();

                }

                return objParam;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }

        }
    }

}
