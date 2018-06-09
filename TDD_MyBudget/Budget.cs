using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_MyBudget
{
    public class Budget
    {
        
        public string Month { get; set; }
        public int Amount { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.ParseExact(this.Month, "yyyyMMdd", CultureInfo.InvariantCulture);
        }


        //public bool IsInRange(DateTime from, DateTime to)
        //{
        //    if (DateTime.Compare(from, to) > 0)
        //    {
        //        return true;
        //    }

        //}


    }
}
