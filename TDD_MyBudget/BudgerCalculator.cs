using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace TDD_MyBudget
{
    public class BudgerCalculator
    {
        private readonly IRepository<Budget> _repo;
        public BudgerCalculator(IRepository<Budget> repo)
        {
            _repo = repo;
        }
        public Decimal ReturnAmount(DateTime StartDate, DateTime EndDate)
        {
            if (DateTime.Compare(EndDate, StartDate) < 0)
            {
                throw new Exception("Illigle date");
            }

            if (StartDate.Year == EndDate.Year && StartDate.Month == EndDate.Month)
            {
                return GetRsult(StartDate, EndDate);
            }

            //foreach (var b in _repo.GetBudget())
            //{
            //    if (EndDate > b.GetDateTime() )
            //    {


            //    }
            // }

            decimal totalBudget = 0;
            for (int year = StartDate.Year; year <= EndDate.Year; year++)
            {
                for (int month = StartDate.Month; month <= EndDate.Month; month++)
                {
                    DateTime startDate = StartDate.Year == year && StartDate.Month == month ? new DateTime(year, month, StartDate.Day) : new DateTime(year, month, 1);
                    DateTime endDate = EndDate.Year == year && EndDate.Month == month ? new DateTime(year, month, EndDate.Day) : new DateTime(year, month,DateTime.DaysInMonth(year, month));
                     totalBudget +=   GetRsult(startDate, endDate);
                }
            }

            return totalBudget;
            return 0;
        }

        private decimal GetRsult(DateTime StartDate, DateTime EndDate)
        {
            var budgetResult = _repo.GetBudget().FirstOrDefault(x => x.Month == string.Format("{0:yyyyMM}", StartDate));

            var budget = (budgetResult == null) ? 0 : budgetResult.Amount;

            var amount = budget * ((EndDate - StartDate).Days + 1) / DateTime.DaysInMonth(StartDate.Year, StartDate.Month);

            return amount;
        }
    }
}
