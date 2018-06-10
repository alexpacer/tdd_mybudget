using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace TDD_MyBudget
{
    [TestClass]
    public class BudgetTests
    {
        public IRepository<Budget> Repo = Substitute.For<IRepository<Budget>>();
        public BudgerCalculator BC;
        public List<Budget> ListofBudget = new List<Budget>
        {
            new Budget {Month = "201806", Amount = 300},
            new Budget {Month = "201807", Amount = 310},
            new Budget {Month = "201602", Amount = 2900},
        };

        [TestInitialize]
        public void Init()
        {
            Repo.GetBudget().Returns(ListofBudget);
            BC = new BudgerCalculator(Repo);
        }

        [TestMethod]
        public void GetFullMonthResult()
        {
            var val = BC.ReturnAmount(ConvertDateTime("20180601"),
                ConvertDateTime("20180630"));
            Assert.AreEqual(300, val);
        }


        [TestMethod]
        public void WrongDatEntered()
        {
            Assert.ThrowsException<Exception>(() => BC.ReturnAmount(ConvertDateTime("20190601"),
                ConvertDateTime("20180601")));
        }

        [TestMethod]
        public void GetOneDayResult()
        {
            var val = BC.ReturnAmount(ConvertDateTime("20180601"),
                ConvertDateTime("20180601"));
            Assert.AreEqual(10, val);
        }

        [TestMethod]
        public void WithOverlappedMonth()
        {
            var val = BC.ReturnAmount(ConvertDateTime("20180601"),
                ConvertDateTime("20180731"));
            Assert.AreEqual(610, val);

        }

        [TestMethod]
        public void GetZeroResult()
        {
            var val = BC.ReturnAmount(ConvertDateTime("20180801"),
                ConvertDateTime("20180815"));
            Assert.AreEqual(0, val);
        }

        [TestMethod]
        public void GetMidMonthResult()
        {
            var val = BC.ReturnAmount(ConvertDateTime("20180615"),
                ConvertDateTime("20180715"));
            Assert.AreEqual(310, val);
        }

        [TestMethod]
        public void GetMidMonthResultWithOutBudget()
        {
            var val = BC.ReturnAmount(ConvertDateTime("20180715"),
                ConvertDateTime("20180815"));
            Assert.AreEqual(170, val);
        }

        [TestMethod]
        public void SpecialFebResult()
        {
            var val = BC.ReturnAmount(ConvertDateTime("20160101"), ConvertDateTime("20160330"));
            Assert.AreEqual(2900, val);

        }

        [TestMethod]
        public void SpecialFebResult_EdingInMidMonth()
        {
            var val = BC.ReturnAmount(ConvertDateTime("20160203"), ConvertDateTime("20160216"));
            Assert.AreEqual(1400, val);

        }

        [TestMethod]
        public void GetFullResult()
        {
            var val = BC.ReturnAmount(ConvertDateTime("20160101"), ConvertDateTime("20181231"));

            Assert.AreEqual(3510, val);
        }

        private DateTime ConvertDateTime(string dateTimeStr)
        {
            return DateTime.ParseExact(dateTimeStr, "yyyyMMdd", CultureInfo.InvariantCulture);

        }
    }
}
