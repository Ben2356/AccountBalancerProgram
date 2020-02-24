using AccountBalancer.Controls.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;

namespace AccountBalancerTests.Controls.Converters
{
    [TestClass]
    public class CurrentStepTextConverterTest
    {
        private CurrentStepTextConverter currentStepConverter = new CurrentStepTextConverter();

        /// <summary>
        /// Tests that an ArgumentException is thrown when the number of values is not 2
        /// </summary>
        [TestMethod]
        public void TestValuesIncorrectCount()
        {
            object[] values = new object[] { null, null, null };
            var exception = Assert.ThrowsException<ArgumentException>(() => currentStepConverter.Convert(values, null, null, null));
            Assert.AreEqual("Incorrect value count", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when values[0] is not an int
        /// </summary>
        [TestMethod]
        public void TestValuesMissingInt()
        {
            object[] values = new object[] { null, new ObservableCollection<string>() };
            var exception = Assert.ThrowsException<ArgumentException>(() => currentStepConverter.Convert(values, null, null, null));
            Assert.AreEqual("values[0] is not an int", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when values[1] is not an ObservableCollection<string>
        /// </summary>
        [TestMethod]
        public void TestValuesMissingObservableCollection()
        {
            object[] values = new object[] { 2, null };
            var exception = Assert.ThrowsException<ArgumentException>(() => currentStepConverter.Convert(values, null, null, null));
            Assert.AreEqual("values[1] is not an ObservableCollection<string>", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentOutOfRangeException is thrown when the index is less than 0
        /// </summary>
        [TestMethod]
        public void TestIndexLessThanZero()
        {
            object[] values = new object[] { -1, new ObservableCollection<string>() { "str1", "str2" } };
            var exception = Assert.ThrowsException<ArgumentOutOfRangeException>(() => currentStepConverter.Convert(values, null, null, null));
            Assert.AreEqual("index -1 is out of bounds, size of collection is 2", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentOutOfRangeException is thrown when the index is greater than size of the collection
        /// </summary>
        [TestMethod]
        public void TestIndexGreaterThanCollectionSize()
        {
            object[] values = new object[] { 2, new ObservableCollection<string>() { "str1", "str2" } };
            var exception = Assert.ThrowsException<ArgumentOutOfRangeException>(() => currentStepConverter.Convert(values, null, null, null));
            Assert.AreEqual("index 2 is out of bounds, size of collection is 2", exception.Message);
        }

        /// <summary>
        /// Tests that the text corresponding to the supplied index is successfully returned
        /// </summary>
        [TestMethod]
        public void TestGetCurrentStepText()
        {
            object[] values = new object[] { 1, new ObservableCollection<string>() { "str1", "str2" } };
            Assert.AreEqual("str2", currentStepConverter.Convert(values, null, null, null));
        }
    }
}
