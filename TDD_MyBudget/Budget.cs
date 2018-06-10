using System;
using System.Globalization;

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
    }
}
