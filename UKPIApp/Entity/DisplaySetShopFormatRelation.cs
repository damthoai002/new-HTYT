using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.Entity
{
    public class DisplaySetShopFormatRelation : BaseEntity
    {
        public string DisplaySetCode { get; set; }
        public string ShopFormat { get; set; }
        
        public DisplaySetShopFormatRelation()
            : base()
        {
            DisplaySetCode = string.Empty;
            ShopFormat = string.Empty;
        }
    }
}
