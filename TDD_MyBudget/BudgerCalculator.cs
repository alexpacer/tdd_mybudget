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
        public decimal ReturnAmount(DateTime start, DateTime end)
        {
            if (DateTime.Compare(end, start) < 0)
            {
                throw new Exception("Illegal date");
            }

            if (start.Year == end.Year && start.Month == end.Month)
            {
                return GetRsult(start, end);
            }

            decimal totalBudget = 0;
            for (var year = start.Year; year <= end.Year; year++)
            {
                for (var month = start.Month; month <= end.Month; month++)
                {
                    var startDate = start.Year == year && start.Month == month
                        ? new DateTime(year, month, start.Day)
                        : new DateTime(year, month, 1);
                    var endDate = end.Year == year && end.Month == month
                        ? new DateTime(year, month, end.Day)
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
