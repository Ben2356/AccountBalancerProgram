using AccountBalancer;
using AccountBalancer.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace AccountBalancerTests.Commands
{
    [TestClass]
    public class AddItemCommandTest
    {
        private List<decimal> itemList;
        private AddItemCommand addItemCommand;

        [TestInitialize]
        public void TestSetup()
        {
            itemList = new List<decimal>();
            addItemCommand = new AddItemCommand(itemList.Add);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when the Action is null
        /// </summary>
        [TestMethod]
        public void TestAddItemCommandNullAction()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => new AddItemCommand(null));
            Assert.AreEqual("AddItem action is null", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when the parameter is not an object array
        /// </summary>
        [TestMethod]
        public void TestCanExecuteParameterIsNotObjectArray()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => addItemCommand.CanExecute(null));
            Assert.AreEqual("parameter is not an object[]", exception.Message);
        }

        /// <summary>
        /// Tests than an ArgumentException is thrown when the parameter array is missing the CurrencyTextBox
        /// </summary>
        [TestMethod]
        public void TestCanExecuteParameterMissingCurrencyTextBox()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => addItemCommand.CanExecute(new object[] { null, new ListBox() }));
            Assert.AreEqual("parameter[0] is not a CurrencyTextBox", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when the parameter array is missing the ListBox
        /// </summary>
        [TestMethod]
        public void TestCanExecuteParameterMissingListBox()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => addItemCommand.CanExecute(new object[] { new CurrencyTextBox(), null }));
            Assert.AreEqual("parameter[1] is not a ListBox", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when the incorrect number of parameters are provided
        /// </summary>
        [TestMethod]
        public void TestCanExecuteParameterIncorrectCount()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => addItemCommand.CanExecute(new object[] { null }));
            Assert.AreEqual("Incorrect parameter count", exception.Message);
        }

        /// <summary>
        /// Tests that the CanExecute function returns false when the supplied TextBox is empty
        /// </summary>
        [TestMethod]
        public void TestCanExecuteEmptyTextBox()
        {
            CurrencyTextBox textBox = new CurrencyTextBox() { Text = "" };
            ListBox listBox = new ListBox();
            object[] parameters = { textBox, listBox };

            Assert.IsFalse(addItemCommand.CanExecute(parameters));
        }

        /// <summary>
        /// Tests that the CanExecute function returns true when the supplied TextBox is not empty
        /// </summary>
        [TestMethod]
        public void TestCanExecuteNonEmptyTextBox()
        {
            CurrencyTextBox textBox = new CurrencyTextBox() { Text = "15.00" };
            ListBox listBox = new ListBox();
            object[] parameters = { textBox, listBox };

            Assert.IsTrue(addItemCommand.CanExecute(parameters));
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when the parameter is not an object array
        /// </summary>
        [TestMethod]
        public void TestExecuteParameterIsNotObjectArray()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => addItemCommand.Execute(null));
            Assert.AreEqual("parameter is not an object[]", exception.Message);
        }

        /// <summary>
        /// Tests than an ArgumentException is thrown when the parameter array is missing the CurrencyTextBox
        /// </summary>
        [TestMethod]
        public void TestExecuteParameterMissingCurrencyTextBox()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => addItemCommand.Execute(new object[] { null, new ListBox() }));
            Assert.AreEqual("parameter[0] is not a CurrencyTextBox", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when the parameter array is missing the ListBox
        /// </summary>
        [TestMethod]
        public void TestExecuteParameterMissingListBox()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => addItemCommand.Execute(new object[] { new CurrencyTextBox(), null }));
            Assert.AreEqual("parameter[1] is not a ListBox", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when the incorrect number of parameters are provided
        /// </summary>
        [TestMethod]
        public void TestExecuteParameterIncorrectCount()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => addItemCommand.Execute(new object[] { null }));
            Assert.AreEqual("Incorrect parameter count", exception.Message);
        }

        /// <summary>
        /// Tests the Execute function successfully executes adds an item and clears the TextBox text property
        /// </summary>
        [TestMethod]
        public void TestExecute()
        {
            TextBox textBox = new CurrencyTextBox() { Text = "15.00" };
            ListBox listBox = new ListBox() { ItemsSource = itemList };
            object[] parameters = { textBox, listBox };
            addItemCommand.Execute(parameters);

            Assert.AreEqual("", textBox.Text);
            Assert.AreEqual(1, itemList.Count);
            Assert.AreEqual(15.00M, itemList[0]);
        }
    }
}
