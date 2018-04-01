using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Model
{
    public class ZBDetail : IEqualityComparer<ZBDetail>
    {
        public string amount { get; set; }

        public string price { get; set; }

        public int tid { get; set; }

        public string type { get; set; }

        public int date { get; set; }

        public string date_string => Program.StartDate.AddTicks(date * 10000000L).ToString("yyyy-MM-dd HH:mm:ss,ms");

        public string trade_type { get; set; }

        public bool Equals(ZBDetail x, ZBDetail y)
        {
            return x.date + x.price == y.date + y.price;
        }

        public int GetHashCode(ZBDetail obj)
        {
            return obj.ToString().GetHashCode();
        }
    }

    public class ZBModel
    {
        public string dataType { get; set; }

        public List<ZBDetail> data { get; set; }
    }
}
