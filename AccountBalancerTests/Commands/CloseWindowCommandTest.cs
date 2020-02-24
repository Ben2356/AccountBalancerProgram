using AccountBalancer.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;

namespace AccountBalancerTests.Commands
{
    [TestClass]
    public class CloseWindowCommandTest
    {
        /// <summary>
        /// Tests that ArgumentException is thrown when the parameter is not the Window
        /// </summary>
        [TestMethod]
        public void TestExecuteWithNonWindowParameter()
        {
            CloseWindowCommand changeFocusCommand = new CloseWindowCommand();
            var exception = Assert.ThrowsException<ArgumentException>(() => changeFocusCommand.Execute(null));
            Assert.AreEqual("parameter is not Window", exception.Message);
        }

        /// <summary>
        /// Tests that the provided window parameter is closed
        /// </summary>
        [TestMethod]
        public void TestExecuteSuccess()
        {
            CloseWindowCommand changeFocusCommand = new CloseWindowCommand();
            Window window = new Window();
            bool windowClosed = false;
            window.Closed += (sender, e) => { windowClosed = true; };
            changeFocusCommand.Execute(window);

            Assert.IsTrue(windowClosed);
        }
    }
}
