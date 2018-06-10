using System;
using System.Linq;

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
                throw new Exception("Illegal date");
            }

            if (StartDate.Year == EndDate.Year && StartDate.Month == EndDate.Month)
            {
                return GetRsult(StartDate, EndDate);
            }

            decimal totalBudget = 0;
            for (var year = StartDate.Year; year <= EndDate.Year; year++)
            {
                for (var month = StartDate.Month; month <= EndDate.Month; month++)
                {
                    var startDate = StartDate.Year == year && StartDate.Month == month
                        ? new DateTime(year, month, StartDate.Day)
                        : new DateTime(year, month, 1);
                    var endDate = EndDate.Year == year && EndDate.Month == month
                        ? new DateTime(year, month, EndDate.Day)
                        : new DateTime(year, month, DateTime.DaysInMonth(year, month));
                    totalBudget += GetRsult(startDate, endDate);
                }
            }

            return totalBudget;
        }

        private decimal GetRsult(DateTime startDate, DateTime endDate)
        {
            var budgetResult = _repo.GetBudget().FirstOrDefault(x => x.Month == $"{startDate:yyyyMM}");

            var budget = (budgetResult == null) ? 0 : budgetResult.Amount;

            var amount = budget * ((endDate - startDate).Days + 1) / DateTime.DaysInMonth(startDate.Year, startDate.Month);

            return amount;
        }
    }
}
