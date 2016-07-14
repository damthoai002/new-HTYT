using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.Entity
{
    public class BasicExtraSetRelation : BaseEntity
    {
        public string BasicSetCode { get; set; }
        public string ExtraSetCode { get; set; }

        public BasicExtraSetRelation()
            : base()
        {
            BasicSetCode = string.Empty;
            ExtraSetCode = string.Empty;
        }
    }
}
