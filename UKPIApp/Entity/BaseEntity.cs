using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.Entity
{
    public class BaseEntity
    {
        public const int DEFAULT_TRUE_VALUE = 1;

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? CreatedOn { get; set; }

        public BaseEntity()
        {
            CreatedBy = string.Empty;
            UpdatedBy = string.Empty;
            UpdatedOn = null;
            CreatedOn = null;
        }
    }
}
