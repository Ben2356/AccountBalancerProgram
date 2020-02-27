using AccountBalancer.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Input;

namespace AccountBalancerTests.Controls
{
    [TestClass]
    public class CurrencyTextBoxTest
    {
        private CurrencyTextBox currencyTextBox;

        [TestInitialize]
        public void Setup()
        {
            currencyTextBox = new CurrencyTextBox();
        }

        /// <summary>
        /// input: 
        /// valid: false
        /// </summary>
        [TestMethod]
        public void TestTextChangedEmpty()
        {
            currencyTextBox.Text = "";
            Assert.IsFalse(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: ab100
        /// valid: false
        /// </summary>
        [TestMethod]
        public void TestTextChangedLetterInput()
        {
            currencyTextBox.Text = "ab100";
            Assert.IsFalse(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 15,gjr.02
        /// valid: false
        /// </summary>
        [TestMethod]
        public void TestTextChangedInvalidMixAlphaNumeric()
        {
            currencyTextBox.Text = "15,gjr.02";
            Assert.IsFalse(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 4.0e2
        /// valid: false
        /// </summary>
        [TestMethod]
        public void TestTextChangedInvalidMixAlphaNumericExp()
        {
            currencyTextBox.Text = "4.0e2";
            Assert.IsFalse(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: -15.00
        /// valid: false
        /// </summary>
        [TestMethod]
        public void TestTextChangedInvalidMixSymbolsNegative()
        {
            currencyTextBox.Text = "-15.00";
            Assert.IsFalse(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 14/2
        /// valid: false
        /// </summary>
        [TestMethod]
        public void TestTextChangedInvalidMixSymbols()
        {
            currencyTextBox.Text = "14/2";
            Assert.IsFalse(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: ,100
        /// valid: false
        /// </summary>
        [TestMethod]
        public void TestTextChangedInvalidCommaUseNoFirstDigit()
        {
            currencyTextBox.Text = ",100";
            Assert.IsFalse(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 4,6211
        /// valid: false
        /// </summary>
        [TestMethod]
        public void TestTextChangedInvalidCommaUseTooManyDigitsAfter()
        {
            currencyTextBox.Text = "4,6211";
            Assert.IsFalse(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 1,25
        /// valid: false
        /// </summary>
        [TestMethod]
        public void TestTextChangedInvalidCommaUseTooFewDigitsAfterTwoDigits()
        {
            currencyTextBox.Text = "1,25";
            Assert.IsFalse(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 1,1
        /// valid: false
        /// </summary>
        [TestMethod]
        public void TestTextChangedInvalidCommaUseTooFewDigitsAfterOneDigit()
        {
            currencyTextBox.Text = "1,1";
            Assert.IsFalse(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 1,
        /// valid: false
        /// </summary>
        [TestMethod]
        public void TestTextChangedInvalidCommaUseTooFewDigitsAfterNoDigits()
        {
            currencyTextBox.Text = "1,";
            Assert.IsFalse(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 45,000
        /// valid: true
        /// </summary>
        [TestMethod]
        public void TestTextChangedValidCommaUseTwoDigitsBefore()
        {
            currencyTextBox.Text = "45,000";
            Assert.IsTrue(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 1,252
        /// valid: true
        /// </summary>
        [TestMethod]
        public void TestTextChangedValidCommaUse()
        {
            currencyTextBox.Text = "1,252";
            Assert.IsTrue(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 1,200,658,000
        /// valid: true
        /// </summary>
        [TestMethod]
        public void TestTextChangedValidCommaUseMultipleCommas()
        {
            currencyTextBox.Text = "1,200,658,000";
            Assert.IsTrue(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 100.005
        /// valid: false
        /// </summary>
        [TestMethod]
        public void TestTextChangedInvalidDecimalUseTooManyDigits()
        {
            currencyTextBox.Text = "100.005";
            Assert.IsFalse(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 20.
        /// valid: true
        /// </summary>
        [TestMethod]
        public void TestTextChangedInvalidDecimalUseNoDigits()
        {
            currencyTextBox.Text = "20.";
            Assert.IsFalse(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 20.1
        /// valid: true
        /// </summary>
        [TestMethod]
        public void TestTextChangedValidDecimalUseOneDigit()
        {
            currencyTextBox.Text = "20.1";
            Assert.IsTrue(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 20.53
        /// valid: true
        /// </summary>
        [TestMethod]
        public void TestTextChangedValidDecimalUseBothDigits()
        {
            currencyTextBox.Text = "20.53";
            Assert.IsTrue(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 200.47
        /// valid: true
        /// </summary>
        [TestMethod]
        public void TestTextChangedValidInput()
        {
            currencyTextBox.Text = "200.47";
            Assert.IsTrue(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 15486619
        /// valid: true
        /// </summary>
        [TestMethod]
        public void TestTextChangedValidInputNoCommas()
        {
            currencyTextBox.Text = "15486619";
            Assert.IsTrue(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 12..
        /// valid: false
        /// </summary>
        [TestMethod]
        public void TestTextChangedInvalidInputTwoDecimals()
        {
            currencyTextBox.Text = "12..";
            Assert.IsFalse(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// input: 12.52.
        /// valid: false
        /// </summary>
        [TestMethod]
        public void TestTextChangedInvalidInputDecimalDigitsDecimal()
        {
            currencyTextBox.Text = "12.52.";
            Assert.IsFalse(currencyTextBox.IsInputValid);
        }

        /// <summary>
        /// Tests that the CurrencyTextBox invalid text won't be formatted when focus is lost
        /// </summary>
        [TestMethod]
        public void TestOnLostFocusWithInvalidInput()
        {
            currencyTextBox.Focus();
            string text = "abc.123";
            currencyTextBox.Text = text;
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope(currencyTextBox), null);
            Keyboard.ClearFocus();
            Assert.AreEqual(text, currencyTextBox.Text);
        }

        /// <summary>
        /// Tests that the CurrencyTextBox valid text is formatted when the focus is lost
        /// </summary>
        [TestMethod]
        public void TestOnLostFocusWithValidInput()
        {
            currencyTextBox.Focus();
            currencyTextBox.Text = "19000.4";
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope(currencyTextBox), null);
            Keyboard.ClearFocus();
            Assert.AreEqual("19,000.40", currencyTextBox.Text);
        }
    }
}
