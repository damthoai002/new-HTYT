using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.BlendedReport.DAL
{
    public class DistributorOutlet
    {
        public string DistributorID { get; set; }
        public string OutletID { get; set; }
        public int Row { get; set; }

        public DistributorOutlet()
        {
            DistributorID = string.Empty;
            OutletID = string.Empty;
            Row = 0;
        }

        public DistributorOutlet(string distributorID, string outletID, int row)
        {
            DistributorID = distributorID;
            OutletID = outletID;
            Row = row;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", DistributorID, OutletID);
        }
    }
}
