using System.Collections.Generic;
using AccountBalancer.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccountBalancerTests.Model
{
    [TestClass]
    public class AccountModelTest
    {
        private AccountModel accountModel;
        private List<string> events;

        [TestInitialize]
        public void TestInitialize()
        {
            accountModel = new AccountModel();
            events = new List<string>();
            accountModel.PropertyChanged += (sender, e) => events.Add(e.PropertyName);
        }

        /// <summary>
        /// Tests that the accountRegisterBalance property changed event was triggered when the account register balance is set
        /// </summary>
        [TestMethod]
        public void TestAccountRegisterBalancePropertyChangedOnSet()
        {
            accountModel.AccountRegisterBalance = 50.00M;
            Assert.AreEqual("accountRegisterBalance", events[0]);
        }

        /// <summary>
        /// Tests that the deduction is added correctly and the total deductions property changed event was triggered
        /// </summary>
        [TestMethod]
        public void TestAddDeductionSingleItem()
        {
            decimal value = 15.00M;

            accountModel.AddDeduction(value);

            Assert.AreEqual(1, accountModel.Deductions.Count);
            Assert.AreEqual(value, accountModel.Deductions[0].Item1);
            Assert.AreEqual(0, accountModel.Deductions[0].Item2);
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual("TotalDeductions", events[0]);
        }

        /// <summary>
        /// Tests that multiple deductions are added correctly and the total deductions property changed event is triggered for each one
        /// </summary>
        [TestMethod]
        public void TestAddDeductionSeveralItems()
        {
            decimal value0 = 50.00M;
            decimal value1 = 20.00M;
            decimal value2 = 35.00M;

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
            Assert.AreEqual(3, events.Count);
            Assert.IsTrue(events.TrueForAll((str) => { return str == "TotalDeductions"; }));
        }

        /// <summary>
        /// Tests that a deduction is removed and the total deductions property changed event is triggered
        /// </summary>
        [TestMethod]
        public void TestRemoveDeductionSingleItem()
        {
            accountModel.AddDeduction(20.00M);
            accountModel.RemoveDeduction(0);

            Assert.AreEqual(0, accountModel.Deductions.Count);
            Assert.AreEqual(2, events.Count);
            Assert.IsTrue(events.TrueForAll((str) => { return str == "TotalDeductions"; }));
        }

        /// <summary>
        /// Tests that multiple deductions are removed and the total deductions property changed event is triggered for each
        /// </summary>
        [TestMethod]
        public void TestRemoveDeductionSeveralItem()
        {
            decimal deductionValue = 50.00M;
            accountModel.AddDeduction(20.00M);
            accountModel.AddDeduction(deductionValue);
            accountModel.AddDeduction(30.00M);
            accountModel.RemoveDeduction(0);
            accountModel.RemoveDeduction(2);

            Assert.AreEqual(1, accountModel.Deductions.Count);
            Assert.AreEqual(deductionValue, accountModel.Deductions[0].Item1);
            Assert.AreEqual(1, accountModel.Deductions[0].Item2);
            Assert.AreEqual(5, events.Count);
            Assert.IsTrue(events.TrueForAll((str) => { return str == "TotalDeductions"; }));
        }

        /// <summary>
        /// Tests that nothing is removed from the deductions collection when the id is not found
        /// </summary>
        [TestMethod]
        public void TestRemoveDeductionBadId()
        {
            decimal deductionValue = 10.00M;
            accountModel.AddDeduction(deductionValue);
            accountModel.RemoveDeduction(2);

            Assert.AreEqual(1, accountModel.Deductions.Count);
            Assert.AreEqual(deductionValue, accountModel.Deductions[0].Item1);
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual("TotalDeductions", events[0]);
        }

        /// <summary>
        /// Tests that nothing is removed from the deductions collection when the collection is empty
        /// </summary>
        [TestMethod]
        public void TestRemoveDeductionEmptyCollection()
        {
            accountModel.RemoveDeduction(0);

            Assert.AreEqual(0, accountModel.Deductions.Count);
            Assert.AreEqual(0, events.Count);
        }

        /// <summary>
        /// Tests that the total deductions sum is 0 when there are no deductions in the collection
        /// </summary>
        [TestMethod]
        public void TestTotalDeductionsEmptyCollection()
        {
            Assert.AreEqual(0, accountModel.TotalDeductions);
        }

        /// <summary>
        /// Tests that the total deductions sum is correct when there are deductions in the collection
        /// </summary>
        [TestMethod]
        public void TestTotalDeductionsNonEmptyCollection()
        {
            accountModel.AddDeduction(15.00M);
            accountModel.AddDeduction(30.00M);
            accountModel.AddDeduction(50.00M);

            Assert.AreEqual(95.00M, accountModel.TotalDeductions);
            Assert.AreEqual(3, events.Count);
            Assert.IsTrue(events.TrueForAll((str) => { return str == "TotalDeductions"; }));
        }

        /// <summary>
        /// Tests that the credit is added correctly and the total credits property changed event was triggered
        /// </summary>
        [TestMethod]
        public void TestAddCreditSingleItem()
        {
            decimal value = 15.00M;

            accountModel.AddCredit(value);

            Assert.AreEqual(1, accountModel.Credits.Count);
            Assert.AreEqual(value, accountModel.Credits[0].Item1);
            Assert.AreEqual(0, accountModel.Credits[0].Item2);
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual("TotalCredits", events[0]);
        }

        /// <summary>
        /// Tests that multiple credits are added correctly and the total credits property changed event is triggered for each
        /// </summary>
        [TestMethod]
        public void TestAddCreditSeveralItems()
        {
            decimal value0 = 50.00M;
            decimal value1 = 20.00M;
            decimal value2 = 35.00M;

            accountModel.AddCredit(value0);
            accountModel.AddCredit(value1);
            accountModel.AddCredit(value2);

            Assert.AreEqual(3, accountModel.Credits.Count);
            Assert.AreEqual(value0, accountModel.Credits[0].Item1);
            Assert.AreEqual(0, accountModel.Credits[0].Item2);
            Assert.AreEqual(value1, accountModel.Credits[1].Item1);
            Assert.AreEqual(1, accountModel.Credits[1].Item2);
            Assert.AreEqual(value2, accountModel.Credits[2].Item1);
            Assert.AreEqual(2, accountModel.Credits[2].Item2);
            Assert.AreEqual(3, events.Count);
            Assert.IsTrue(events.TrueForAll((str) => { return str == "TotalCredits"; }));
        }

        /// <summary>
        /// Tests that a credit is removed and the total credits property changed event is triggered
        /// </summary>
        [TestMethod]
        public void TestRemoveCreditSingleItem()
        {
            accountModel.AddCredit(20.00M);
            accountModel.RemoveCredit(0);

            Assert.AreEqual(0, accountModel.Credits.Count);
            Assert.AreEqual(2, events.Count);
            Assert.IsTrue(events.TrueForAll((str) => { return str == "TotalCredits"; }));
        }

        /// <summary>
        /// Tests that multiple credits are removed and the total credits property changed event is triggered for each
        /// </summary>
        [TestMethod]
        public void TestRemoveCreditSeveralItem()
        {
            decimal creditValue = 50.00M;
            accountModel.AddCredit(20.00M);
            accountModel.AddCredit(creditValue);
            accountModel.AddCredit(30.00M);
            accountModel.RemoveCredit(0);
            accountModel.RemoveCredit(2);

            Assert.AreEqual(1, accountModel.Credits.Count);
            Assert.AreEqual(creditValue, accountModel.Credits[0].Item1);
            Assert.AreEqual(1, accountModel.Credits[0].Item2);
            Assert.AreEqual(5, events.Count);
            Assert.IsTrue(events.TrueForAll((str) => { return str == "TotalCredits"; }));
        }

        /// <summary>
        /// Tests that nothing is removed from the credits collection when the id is not found
        /// </summary>
        [TestMethod]
        public void TestRemoveCreditBadId()
        {
            decimal creditsValue = 10.00M;
            accountModel.AddCredit(creditsValue);
            accountModel.RemoveCredit(2);

            Assert.AreEqual(1, accountModel.Credits.Count);
            Assert.AreEqual(creditsValue, accountModel.Credits[0].Item1);
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual("TotalCredits", events[0]);
        }

        /// <summary>
        /// Tests that nothing is removed from the credits collection when the collection is empty
        /// </summary>
        [TestMethod]
        public void TestRemoveCreditEmptyCollection()
        {
            accountModel.RemoveCredit(0);

            Assert.AreEqual(0, accountModel.Credits.Count);
            Assert.AreEqual(0, events.Count);
        }

        /// <summary>
        /// Tests that the total credits sum is 0 when there are no credits in the collection
        /// </summary>
        [TestMethod]
        public void TestTotalCreditsEmptyCollection()
        {
            Assert.AreEqual(0, accountModel.TotalCredits);
        }

        /// <summary>
        /// Tests that the total credits sum is correct when there are credits in the collection
        /// </summary>
        [TestMethod]
        public void TestTotalCreditsNonEmptyCollection()
        {
            accountModel.AddCredit(15.00M);
            accountModel.AddCredit(30.00M);
            accountModel.AddCredit(50.00M);

            Assert.AreEqual(95.00M, accountModel.TotalCredits);
            Assert.AreEqual(3, events.Count);
            Assert.IsTrue(events.TrueForAll((str) => { return str == "TotalCredits"; }));
        }

        /// <summary>
        /// Tests that the new account register balance is 0 when none of the required fields are set
        /// </summary>
        [TestMethod]
        public void TestNewAccountRegisterBalanceEmptyFields()
        {
            Assert.AreEqual(0, accountModel.NewAccountRegisterBalance);
        }

        /// <summary>
        /// Tests that the new account register balance is correct when the required fields contain data
        /// </summary>
        [TestMethod]
        public void TestNewAccountRegisterBalanceNonEmptyFields()
        {
            accountModel.AccountRegisterBalance = 50.00M;
            accountModel.AddDeduction(5.00M);
            accountModel.AddDeduction(10.00M);
            accountModel.AddCredit(20.00M);
            Assert.AreEqual(15.00M, accountModel.NewAccountRegisterBalance);
        }

        /// <summary>
        /// Tests that the statement ending balance property changed event was triggered when the statement ending balance is set
        /// </summary>
        [TestMethod]
        public void TestStatementEndingBalancePropertyChangedOnSet()
        {
            accountModel.StatementEndingBalance = 20.00M;
            Assert.AreEqual("StatementEndingBalance", events[0]);
        }

        /// <summary>
        /// Tests that the deposit is added correctly and the total deposits property changed event was triggered
        /// </summary>
        [TestMethod]
        public void TestAddDepositsSingleItem()
        {
            decimal value = 15.00M;

            accountModel.AddDeposit(value);

            Assert.AreEqual(1, accountModel.Deposits.Count);
            Assert.AreEqual(value, accountModel.Deposits[0].Item1);
            Assert.AreEqual(0, accountModel.Deposits[0].Item2);
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual("TotalDeposits", events[0]);
        }

        /// <summary>
        /// Tests that multiple deposits are added correctly and the total deposits property changed event is triggered for each one
        /// </summary>
        [TestMethod]
        public void TestAddDepositsSeveralItems()
        {
            decimal value0 = 50.00M;
            decimal value1 = 20.00M;
            decimal value2 = 35.00M;

            accountModel.AddDeposit(value0);
            accountModel.AddDeposit(value1);
            accountModel.AddDeposit(value2);

            Assert.AreEqual(3, accountModel.Deposits.Count);
            Assert.AreEqual(value0, accountModel.Deposits[0].Item1);
            Assert.AreEqual(0, accountModel.Deposits[0].Item2);
            Assert.AreEqual(value1, accountModel.Deposits[1].Item1);
            Assert.AreEqual(1, accountModel.Deposits[1].Item2);
            Assert.AreEqual(value2, accountModel.Deposits[2].Item1);
            Assert.AreEqual(2, accountModel.Deposits[2].Item2);
            Assert.AreEqual(3, events.Count);
            Assert.IsTrue(events.TrueForAll((str) => { return str == "TotalDeposits"; }));
        }

        /// <summary>
        /// Tests that a deposit is removed and the total deposit property changed event is triggered
        /// </summary>
        [TestMethod]
        public void TestRemoveDepositsSingleItem()
        {
            accountModel.AddDeposit(20.00M);
            accountModel.RemoveDeposit(0);

            Assert.AreEqual(0, accountModel.Deposits.Count);
            Assert.AreEqual(2, events.Count);
            Assert.IsTrue(events.TrueForAll((str) => { return str == "TotalDeposits"; }));
        }

        /// <summary>
        /// Tests that multiple deposits are removed and the total deposits property changed event is triggered for each
        /// </summary>
        [TestMethod]
        public void TestRemoveDepositSeveralItem()
        {
            decimal depositValue = 50.00M;
            accountModel.AddDeposit(20.00M);
            accountModel.AddDeposit(depositValue);
            accountModel.AddDeposit(30.00M);
            accountModel.RemoveDeposit(0);
            accountModel.RemoveDeposit(2);

            Assert.AreEqual(1, accountModel.Deposits.Count);
            Assert.AreEqual(depositValue, accountModel.Deposits[0].Item1);
            Assert.AreEqual(1, accountModel.Deposits[0].Item2);
            Assert.AreEqual(5, events.Count);
            Assert.IsTrue(events.TrueForAll((str) => { return str == "TotalDeposits"; }));
        }

        /// <summary>
        /// Tests that nothing is removed from the deposits collection when the id is not found
        /// </summary>
        [TestMethod]
        public void TestRemoveDepositBadId()
        {
            decimal depositValue = 10.00M;
            accountModel.AddDeposit(depositValue);
            accountModel.RemoveDeposit(2);

            Assert.AreEqual(1, accountModel.Deposits.Count);
            Assert.AreEqual(depositValue, accountModel.Deposits[0].Item1);
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual("TotalDeposits", events[0]);
        }

        /// <summary>
        /// Tests that nothing is removed from the deposits collection when the collection is empty
        /// </summary>
        [TestMethod]
        public void TestRemoveDepositEmptyCollection()
        {
            accountModel.RemoveDeposit(0);

            Assert.AreEqual(0, accountModel.Deposits.Count);
            Assert.AreEqual(0, events.Count);
        }

        /// <summary>
        /// Tests that the total deposits sum is 0 when there are deposits in the collection
        /// </summary>
        [TestMethod]
        public void TestTotalDepositsEmptyCollection()
        {
            Assert.AreEqual(0, accountModel.TotalDeposits);
        }

        /// <summary>
        /// Tests that the total deposits are correct when there are deposits in the collection
        /// </summary>
        [TestMethod]
        public void TestTotalDepositsNonEmptyCollection()
        {
            accountModel.AddDeposit(15.00M);
            accountModel.AddDeposit(30.00M);
            accountModel.AddDeposit(50.00M);

            Assert.AreEqual(95.00M, accountModel.TotalDeposits);
            Assert.AreEqual(3, events.Count);
            Assert.IsTrue(events.TrueForAll((str) => { return str == "TotalDeposits"; }));
        }

        /// <summary>
        /// Tests that the withdrawal is added correctly and the total withdrawals property changed event was triggered
        /// </summary>
        [TestMethod]
        public void TestAddWithdrawalSingleItem()
        {
            decimal value = 15.00M;

            accountModel.AddWithdrawal(value);

            Assert.AreEqual(1, accountModel.Withdrawals.Count);
            Assert.AreEqual(value, accountModel.Withdrawals[0].Item1);
            Assert.AreEqual(0, accountModel.Withdrawals[0].Item2);
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual("TotalWithdrawals", events[0]);
        }

        /// <summary>
        /// Tests that multiple withdrawals are added correctly and the total withdrawals property changed event is triggered for each one
        /// </summary>
        [TestMethod]
        public void TestAddWithdrawalSeveralItems()
        {
            decimal value0 = 50.00M;
            decimal value1 = 20.00M;
            decimal value2 = 35.00M;

            accountModel.AddWithdrawal(value0);
            accountModel.AddWithdrawal(value1);
            accountModel.AddWithdrawal(value2);

            Assert.AreEqual(3, accountModel.Withdrawals.Count);
            Assert.AreEqual(value0, accountModel.Withdrawals[0].Item1);
            Assert.AreEqual(0, accountModel.Withdrawals[0].Item2);
            Assert.AreEqual(value1, accountModel.Withdrawals[1].Item1);
            Assert.AreEqual(1, accountModel.Withdrawals[1].Item2);
            Assert.AreEqual(value2, accountModel.Withdrawals[2].Item1);
            Assert.AreEqual(2, accountModel.Withdrawals[2].Item2);
            Assert.AreEqual(3, events.Count);
            Assert.IsTrue(events.TrueForAll((str) => { return str == "TotalWithdrawals"; }));
        }

        /// <summary>
        /// Tests that a withdrawal is removed and the total withdrawal property changed event is triggered
        /// </summary>
        [TestMethod]
        public void TestRemoveWithdrawalSingleItem()
        {
            accountModel.AddWithdrawal(20.00M);
            accountModel.RemoveWithdrawal(0);

            Assert.AreEqual(0, accountModel.Withdrawals.Count);
            Assert.AreEqual(2, events.Count);
            Assert.IsTrue(events.TrueForAll((str) => { return str == "TotalWithdrawals"; }));
        }

        /// <summary>
        /// Tests that multiple withdrawals are removed and the total withdrawals property changed event is triggered for each
        /// </summary>
        [TestMethod]
        public void TestRemoveWithdrawalSeveralItem()
        {
            decimal withdrawalValue = 50.00M;
            accountModel.AddWithdrawal(20.00M);
            accountModel.AddWithdrawal(withdrawalValue);
            accountModel.AddWithdrawal(30.00M);
            accountModel.RemoveWithdrawal(0);
            accountModel.RemoveWithdrawal(2);

            Assert.AreEqual(1, accountModel.Withdrawals.Count);
            Assert.AreEqual(withdrawalValue, accountModel.Withdrawals[0].Item1);
            Assert.AreEqual(1, accountModel.Withdrawals[0].Item2);
            Assert.AreEqual(5, events.Count);
            Assert.IsTrue(events.TrueForAll((str) => { return str == "TotalWithdrawals"; }));
        }

        /// <summary>
        /// Tests that nothing is removed from the withdrawals collection when the id is not found
        /// </summary>
        [TestMethod]
        public void TestRemoveWithdrawalBadId()
        {
            decimal withdrawalValue = 10.00M;
            accountModel.AddWithdrawal(withdrawalValue);
            accountModel.RemoveWithdrawal(2);

            Assert.AreEqual(1, accountModel.Withdrawals.Count);
            Assert.AreEqual(withdrawalValue, accountModel.Withdrawals[0].Item1);
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual("TotalWithdrawals", events[0]);
        }

        /// <summary>
        /// Tests that nothing is removed from the withdrawals collection when the collection is empty
        /// </summary>
        [TestMethod]
        public void TestRemoveWithdrawalEmptyCollection()
        {
            accountModel.RemoveWithdrawal(0);

            Assert.AreEqual(0, accountModel.Withdrawals.Count);
            Assert.AreEqual(0, events.Count);
        }

        /// <summary>
        /// Tests that the total withdrawals sum is 0 when there are no withdrawals in the collection
        /// </summary>
        [TestMethod]
        public void TestTotalWithdrawalsEmptyCollection()
        {
            Assert.AreEqual(0, accountModel.TotalWithdrawals);
        }

        /// <summary>
        /// Tests that the total withdrawals sum is correct when there are withdrawals in the collection
        /// </summary>
        [TestMethod]
        public void TestTotalWithdrawalsNonEmptyCollection()
        {
            accountModel.AddWithdrawal(15.00M);
            accountModel.AddWithdrawal(30.00M);
            accountModel.AddWithdrawal(50.00M);

            Assert.AreEqual(95.00M, accountModel.TotalWithdrawals);
            Assert.AreEqual(3, events.Count);
            Assert.IsTrue(events.TrueForAll((str) => { return str == "TotalWithdrawals"; }));
        }

        /// <summary>
        /// Tests that the account statement subtotal is 0 when none of the required fields are set
        /// </summary>
        [TestMethod]
        public void TestAccountStatementSubtotalEmptyFields()
        {
            Assert.AreEqual(0, accountModel.AccountStatementSubtotal);
        }

        /// <summary>
        /// Tests that the account statement subtotal is correct when the required fields contain data
        /// </summary>
        [TestMethod]
        public void TestAccountStatementSubtotalNonEmptyFields()
        {
            accountModel.StatementEndingBalance = 20.00M;
            accountModel.AddDeposit(10.00M);
            accountModel.AddDeposit(30.00M);

            Assert.AreEqual(60.00M, accountModel.AccountStatementSubtotal);
        }

        /// <summary>
        /// Tests that the total is 0 when none of the required fields are set
        /// </summary>
        [TestMethod]
        public void TestTotalEmptyFields()
        {
            Assert.AreEqual(0, accountModel.Total);
        }

        /// <summary>
        /// Tests that the total is correct when the required fields contain data
        /// </summary>
        [TestMethod]
        public void TestTotalNonEmptyFields()
        {
            accountModel.StatementEndingBalance = 30.00M;
            accountModel.AddDeposit(50.00M);
            accountModel.AddWithdrawal(10.00M);

            Assert.AreEqual(70.00M, accountModel.Total);
        }
    }
}
