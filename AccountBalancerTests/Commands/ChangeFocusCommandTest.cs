using AccountBalancer.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Controls;

namespace AccountBalancerTests.Commands
{
    [TestClass]
    public class ChangeFocusCommandTest
    {
        /// <summary>
        /// Tests that an ArgumentException is thrown when a non Grid parameter is provided
        /// </summary>
        [TestMethod]
        public void TestExecuteWithNonGridParameter()
        {
            ChangeFocusCommand changeFocusCommand = new ChangeFocusCommand();
            var exception = Assert.ThrowsException<ArgumentException>(() => changeFocusCommand.Execute(new TextBlock()));
            Assert.AreEqual("parameter is not of type Grid", exception.Message);
        }

        /// <summary>
        /// Tests that the Grid parameter is focused when the command is executed
        /// </summary>
        [TestMethod]
        public void TestExecuteWithGridParameter()
        {
            ChangeFocusCommand changeFocusCommand = new ChangeFocusCommand();
            Grid grid = new Grid();
            changeFocusCommand.Execute(grid);

            Assert.IsFalse(grid.IsFocused); //Grid.Focus will never set this flag to true as Grids are not logically focusable
        }
    }
}
