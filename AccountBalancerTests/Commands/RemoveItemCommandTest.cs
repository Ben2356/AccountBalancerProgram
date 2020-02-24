using AccountBalancer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace AccountBalancerTests.Commands
{
    [TestClass]
    public class RemoveItemCommandTest
    {
        /// <summary>
        /// Tests that an ArgumentException is thrown when the RemoveItem action is null
        /// </summary>
        [TestMethod]
        public void TestRemoveItemCommandNullAction()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => { new RemoveItemCommand(null); });
            Assert.AreEqual("RemoveItem action is null", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when the parameter is not a ListBoxItem
        /// </summary>
        [TestMethod]
        public void TestExecuteParameterIsNotListBoxItem()
        {
            List<decimal> itemList = new List<decimal>();
            RemoveItemCommand removeItemCommand = new RemoveItemCommand(itemList.RemoveAt);
            var exception = Assert.ThrowsException<ArgumentException>(() => { removeItemCommand.Execute(new TextBox()); });
            Assert.AreEqual("parameter is not a ListBoxItem", exception.Message);
        }

        /// <summary>
        /// Tests the Execute function successfully removes the ListBoxItem from a collection
        /// </summary>
        [TestMethod]
        public void TestExecute()
        {
            List<decimal> itemList = new List<decimal>() { 15.00M, 50.00M, 20.00M };
            RemoveItemCommand removeItemCommand = new RemoveItemCommand(itemList.RemoveAt);
            ListBoxItem listBoxItem = new ListBoxItem() { Content = new Tuple<decimal, int>(50.00M, 1) };
            removeItemCommand.Execute(listBoxItem);

            Assert.AreEqual(2, itemList.Count);
            Assert.AreEqual(15.00M, itemList[0]);
            Assert.AreEqual(20.00M, itemList[1]);
        }
    }
}
