using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.Entity
{
    public class DisplayResult : BaseEntity
    {
        public DisplayResult()
            : base()
        {
            ProgramCode = string.Empty;
            ShipToCode = string.Empty;
            DisplaySetCode = string.Empty;
            StoreCode = string.Empty;
            CategoryCode = string.Empty;
            Result = string.Empty;
            Comment = string.Empty;
        }

        public string ProgramCode { get; set; }
        public string ShipToCode { get; set; }
        public string DisplaySetCode { get; set; }
        public string StoreCode { get; set; }
        public string CategoryCode { get; set; }
        public string Result { get; set; }
        public string Comment { get; set; }
    }

    public enum AuditResult
    {
        P,
        F
    }
}
