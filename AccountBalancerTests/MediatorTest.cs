using AccountBalancer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AccountBalancerTests
{
    [TestClass]
    public class MediatorTest
    {
        private Mediator mediator;

        [TestInitialize]
        public void Setup()
        {
            mediator = new Mediator();
        }

        /// <summary>
        /// Tests that an ArgumentNullException is thrown when the provided token is null when calling Add
        /// </summary>
        [TestMethod]
        public void TestAddNullToken()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => mediator.Add(null, (arg) => { }));
            Assert.AreEqual("token", exception.ParamName);
        }

        /// <summary>
        /// Tests that an ArgumentNullException is thrown when the provided action is null
        /// </summary>
        [TestMethod]
        public void TestAddNullAction()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => mediator.Add("token", null));
            Assert.AreEqual("action", exception.ParamName);
        }

        /// <summary>
        /// Tests that adding an action adds the action with the provided token and it can be invoked with that token
        /// </summary>
        [TestMethod]
        public void TestInvokeAddThenInvokeAction()
        {
            List<int> list = new List<int>();
            mediator.Add("token", (arg) => list.Add(1));
            mediator.Invoke("token");
            Assert.AreEqual(1, list[0]);
        }

        /// <summary>
        /// Tests that adding an action with a the same token as another action will overwrite that old action with the new one
        /// </summary>
        [TestMethod]
        public void TestAddActionWithSameToken()
        {
            List<int> list = new List<int>();
            mediator.Add("token", (arg) => list.Add(5));
            mediator.Add("token", (arg) => list.Add(100));
            mediator.Invoke("token");
            Assert.AreEqual(100, list[0]);
        }

        /// <summary>
        /// Tests that an ArgumentNullException is thrown when the provided token is null when calling Invoke
        /// </summary>
        [TestMethod]
        public void TestInvokeNullToken()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => mediator.Invoke(null));
            Assert.AreEqual("token", exception.ParamName);
        }

        /// <summary>
        /// Tests that a MissingMethodException is thrown when there is no action for the provided token
        /// </summary>
        [TestMethod]
        public void TestInvokeNonExistingAction()
        {
            var exception = Assert.ThrowsException<MissingMethodException>(() => mediator.Invoke("token"));
            Assert.AreEqual("token does not exist", exception.Message);
        }
    }
}
