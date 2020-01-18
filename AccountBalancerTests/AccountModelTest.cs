using System;
using AccountBalancer.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccountBalancerTests
{
    [TestClass]
    public class AccountModelTest
    {
        private AccountModel accountModel;

        [TestInitialize]
        public void TestInitialize()
        {
            accountModel = new AccountModel();
        }

        [TestMethod]
        public void AddDeductionSingleItemTest()
        {
            float value = 15.00F;

            accountModel.AddDeduction(value);

            Assert.AreEqual(1, accountModel.Deductions.Count);
            Assert.AreEqual(value, accountModel.Deductions[0].Item1);
            Assert.AreEqual(0, accountModel.Deductions[0].Item2);
        }

        [TestMethod]
        public void AddDeductionSeveralItemTest()
        {
            float value0 = 50.00F;
            float value1 = 20.00F;
            float value2 = 35.00F;

            accountModel.AddDeduction(value0);
            accountModel.AddDeduction(value1);
            accountModel.AddDeduction(value2);

            Assert.AreEqual(3, accountModel.Deductions.Count);
            Assert.AreEqual(value0, accountModel.Deductions[0].Item1);
            Assert.AreEqual(0, accountModel.Deductions[0].Item2);
            Assert.AreEqual(value1, accountModel.Deductions[1].Item1);
            Assert.AreEqual(1, accountModel.Deductions[1].Item2);
            Assert.AreEqual(value2, accountModel.Deductions[2].Item1);
            Assert.AreEqual(2, accountModel.Deductions[2].Item2);
        }

        [TestMethod]
        public void RemoveDeductionSingleItemTest()
        {
            accountModel.AddDeduction(20.00F);
            accountModel.RemoveDeduction(0);

            Assert.AreEqual(0, accountModel.Deductions.Count);
        }

        [TestMethod]
        public void RemoveDeductionSeveralItemTest()
        {
            float deductionValue = 50.00F;
            accountModel.AddDeduction(20.00F);
            accountModel.AddDeduction(deductionValue);
            accountModel.AddDeduction(30.00F);
            accountModel.RemoveDeduction(0);
            accountModel.RemoveDeduction(2);

            Assert.AreEqual(1, accountModel.Deductions.Count);
            Assert.AreEqual(deductionValue, accountModel.Deductions[0].Item1);
            Assert.AreEqual(1, accountModel.Deductions[0].Item2);
        }

        [TestMethod]
        public void RemoveDeductionBadIdTest()
        {
            float deductionValue = 10.00F;
            accountModel.AddDeduction(deductionValue);
            accountModel.RemoveDeduction(2);

            Assert.AreEqual(1, accountModel.Deductions.Count);
            Assert.AreEqual(deductionValue, accountModel.Deductions[0].Item1);
        }

        [TestMethod]
        public void RemoveDeductionEmptyCollectionTest()
        {
            accountModel.RemoveDeduction(0);

            Assert.AreEqual(0, accountModel.Deductions.Count);
        }

        [TestMethod]
        public void TotalDeductionsEmptyCollectionTest()
        {
            Assert.AreEqual(0, accountModel.TotalDeductions);
        }

        [TestMethod]
        public void TotalDeductionsNonEmptyCollectionTest()
        {

        } 
    }
}
