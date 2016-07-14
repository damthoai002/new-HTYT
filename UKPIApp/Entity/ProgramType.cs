using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.Entity
{
    public class ProgramType
    {
        public string Type { get; set; }
        public string Description { get; set; }

        public ProgramType()
        {
            Type = string.Empty;
            Description = string.Empty;
        }
    }
}
