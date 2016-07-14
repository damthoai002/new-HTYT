using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class CustomKey
    {
            private string MaThuocYTeHienThi;
            private bool BaoHiem;
            public CustomKey(string MaThuocYTeHienThi, bool BaoHiem)
            {
                this.MaThuocYTeHienThi = MaThuocYTeHienThi;
                this.BaoHiem = BaoHiem;
            }

            public class EqualityComparer : IEqualityComparer<CustomKey>
            {

                public bool Equals(CustomKey x, CustomKey y)
                {
                    return x.MaThuocYTeHienThi == y.MaThuocYTeHienThi && x.BaoHiem == y.BaoHiem;
                }
                //public override CustomKey GetHasCode(CustomKey x)
                //{
                //    return x;
                //}
                public  int GetHashCode(CustomKey x)
                {
                    return x.MaThuocYTeHienThi.GetHashCode() ^ x.BaoHiem.GetHashCode();
                }
            }
    }
}
