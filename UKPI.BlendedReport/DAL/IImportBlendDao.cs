using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.BlendedReport.DAL
{
    public interface IImportBlendDao
    {
        string TableName { get; }
        System.Data.DataTable TableSchema { get; }
        List<string> GetNotExistedDistributor(List<string> distributorCodes);
        List<string> GetNotExistedOutlet(List<string> outletCodes);
        bool SaveToDatabase(System.Data.DataTable table);
    }
}
