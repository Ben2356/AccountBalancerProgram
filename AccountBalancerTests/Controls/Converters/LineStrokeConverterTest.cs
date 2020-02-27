using AccountBalancer.Controls.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace AccountBalancerTests.Controls.Converters
{
    [TestClass]
    public class LineStrokeConverterTest
    {
        private LineStrokeConverter lineStrokeConverter = new LineStrokeConverter();

        /// <summary>
        /// Tests that an ArgumentException is thrown when the number of values is not 3
        /// </summary>
        [TestMethod]
        public void TestValuesIncorrectCount()
        {
            object[] values = new object[] { null, null };
            var exception = Assert.ThrowsException<ArgumentException>(() => lineStrokeConverter.Convert(values, null, true, null));
            Assert.AreEqual("Incorrect value count", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when values[0] is not an int
        /// </summary>
        [TestMethod]
        public void TestValuesMissingInt()
        {
            object[] values = new object[] { null, new ObservableCollection<string>(), "" };
            var exception = Assert.ThrowsException<ArgumentException>(() => lineStrokeConverter.Convert(values, null, true, null));
            Assert.AreEqual("values[0] is not an int", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when values[1] is not an ObservableCollection
        /// </summary>
        [TestMethod]
        public void TestValuesMissingObservableCollection()
        {
            object[] values = new object[] { 1, null, "" };
            var exception = Assert.ThrowsException<ArgumentException>(() => lineStrokeConverter.Convert(values, null, true, null));
            Assert.AreEqual("values[1] is not an ObservableCollection<string>", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when values[2] is not a string
        /// </summary>
        [TestMethod]
        public void TestValuesMissingString()
        {
            object[] values = new object[] { 1, new ObservableCollection<string>(), null };
            var exception = Assert.ThrowsException<ArgumentException>(() => lineStrokeConverter.Convert(values, null, true, null));
            Assert.AreEqual("values[2] is not a string", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when the parameter is not a boolean
        /// </summary>
        [TestMethod]
        public void TestParameterNotBoolType()
        {
            object[] values = new object[] { 1, new ObservableCollection<string>(), "" };
            var exception = Assert.ThrowsException<ArgumentException>(() => lineStrokeConverter.Convert(values, null, null, null));
            Assert.AreEqual("parameter is not a bool", exception.Message);
            Assert.IsTrue(exception.InnerException is NullReferenceException);
        }

        /// <summary>
        /// Tests that green is returned for the left line if the step is done
        /// </summary>
        [TestMethod]
        public void TestLineStrokeLeftLineStepDone()
        {
            object[] values = new object[] { 1, new ObservableCollection<string>() { "str0", "str1", "str2" }, "str0" };
            Assert.AreEqual(new SolidColorBrush(Colors.Green).ToString(), lineStrokeConverter.Convert(values, null, true, null).ToString());
        }

        /// <summary>
        /// Tests that green is returned for the right line if the step is done
        /// </summary>
        [TestMethod]
        public void TestLineStrokeRightLineStepDone()
        {
            object[] values = new object[] { 1, new ObservableCollection<string>() { "str0", "str1", "str2" }, "str0" };
            Assert.AreEqual(new SolidColorBrush(Colors.Green).ToString(), lineStrokeConverter.Convert(values, null, false, null).ToString());
        }

        /// <summary>
        /// Tests that green is returned for the left line if the step is in progress
        /// </summary>
        [TestMethod]
        public void TestLineStrokeLeftLineStepInProgress()
        {
            object[] values = new object[] { 1, new ObservableCollection<string>() { "str0", "str1", "str2" }, "str1" };
            Assert.AreEqual(new SolidColorBrush(Colors.Green).ToString(), lineStrokeConverter.Convert(values, null, true, null).ToString());
        }

        /// <summary>
        /// Tests that gray is returned for the right line if the step is in progress
        /// </summary>
        [TestMethod]
        public void TestLineStrokeRightLineStepInProgress()
        {
            object[] values = new object[] { 1, new ObservableCollection<string>() { "str0", "str1", "str2" }, "str1" };
            Assert.AreEqual(new SolidColorBrush(Colors.Gray).ToString(), lineStrokeConverter.Convert(values, null, false, null).ToString());
        }

        /// <summary>
        /// Tests that gray is returned for the left line if the step is not done
        /// </summary>
        [TestMethod]
        public void TestLineStrokeLeftLineStepNotDone()
        {
            object[] values = new object[] { 1, new ObservableCollection<string>() { "str0", "str1", "str2" }, "str2" };
            Assert.AreEqual(new SolidColorBrush(Colors.Gray).ToString(), lineStrokeConverter.Convert(values, null, true, null).ToString());
        }

        /// <summary>
        /// Tests that gray is returned for the right line if the step is not done
        /// </summary>
        [TestMethod]
        public void TestLineStrokeRightLineStepNotDone()
        {
            object[] values = new object[] { 1, new ObservableCollection<string>() { "str0", "str1", "str2" }, "str2" };
            Assert.AreEqual(new SolidColorBrush(Colors.Gray).ToString(), lineStrokeConverter.Convert(values, null, false, null).ToString());
        }
    }
}
