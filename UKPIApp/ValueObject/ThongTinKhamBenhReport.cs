using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class ThongTinKhamBenhReport
    {
        public ThongTinKhamBenhReportHeader Header { get; set; }
        public List<ThongTinKhamBenhReportDetail> Details { get; set; }
    }
}
