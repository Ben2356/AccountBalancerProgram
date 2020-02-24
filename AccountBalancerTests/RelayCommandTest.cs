using AccountBalancer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace AccountBalancerTests
{
    [TestClass]
    public class RelayCommandTest
    {
        private RelayCommand<object> relayCommand;

        /// <summary>
        /// Tests that an ArgumentNullException is thrown when the execute parameter is null upon creation of a RelayCommand
        /// </summary>
        [TestMethod]
        public void TestCreationNullExecute()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new RelayCommand<object>(null));
            Assert.AreEqual("execute", exception.ParamName);
        }

        /// <summary>
        /// Tests that CanExecute defaults to true when not provided/null at creation
        /// </summary>
        [TestMethod]
        public void TestNoCanExecuteParameterCanExecuteTrue()
        {
            relayCommand = new RelayCommand<object>((arg) => { });
            Assert.AreEqual(true, relayCommand.CanExecute(null));
        }

        /// <summary>
        /// Tests that CanExecute returns false when provided a false predicate at creation
        /// </summary>
        [TestMethod]
        public void TestProvidedCanExecuteFalse()
        {
            relayCommand = new RelayCommand<object>((arg) => { }, (arg) => { return false; });
            Assert.AreEqual(false, relayCommand.CanExecute(null));
        }

        /// <summary>
        /// Tests that CanExecute returns true when provided a true predicate at creation
        /// </summary>
        [TestMethod]
        public void TestProvidedCanExecuteTrue()
        {
            relayCommand = new RelayCommand<object>((arg) => { }, (arg) => { return true; });
            Assert.AreEqual(true, relayCommand.CanExecute(null));
        }

        public void TestExecute()
        {
            List<int> list = new List<int>();
            TestCommand testCommand = new TestCommand(list);
            relayCommand = new RelayCommand<object>(testCommand.Execute);
            relayCommand.Execute(5);
            Assert.AreEqual(5, list[0]);
        }

        private class TestCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;
            private readonly List<int> list;

            public TestCommand(List<int> list)
            {
                this.list = list;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                list.Add((int)parameter);
            }
        }
    }
}
