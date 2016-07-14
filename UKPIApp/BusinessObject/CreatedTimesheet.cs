using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using UKPI.DataAccessObject;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.BusinessObject
{
    public class CreatedTimesheet
    {
        private CreatedTimesheetDao _createTimesheetDao = new CreatedTimesheetDao();
        private clsCommon _common = new clsCommon();


        public DataTable GetSchemaTable()
        {
            return _createTimesheetDao.GetSchemaTable();
        }
    }
}
