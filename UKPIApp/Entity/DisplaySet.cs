using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace UKPI.Entity
{
    public class DisplaySet : BaseEntity
    {
        [XmlElement("DisplaySetCode")]
        public string Code { get; set; }

        [XmlElement("DisplaySetName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Iconic { get; set; }
        public int Extra { get; set; }
        public int Active { get; set; }
        public int DisplayOrder { get; set; }
        public decimal TurnOverLimit { get; set; }
        public decimal IncentiveValue { get; set; }

        public DisplaySet()
            : base()
        {
            Code = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Iconic = 0;
            Extra = 0;
            Active = DEFAULT_TRUE_VALUE;
            TurnOverLimit = 0;
            IncentiveValue = 0;
            DisplayOrder = 0;
        }
    }
}
