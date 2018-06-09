using System;
using System.Collections.Generic;

namespace TDD_MyBudget
{
    public interface IRepository<T>
    {
        List<Budget> GetBudget();
    }
}
