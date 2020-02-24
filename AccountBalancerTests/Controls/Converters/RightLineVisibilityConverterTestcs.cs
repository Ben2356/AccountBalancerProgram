using AccountBalancer.Controls.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace AccountBalancerTests.Controls.Converters
{
    [TestClass]
    public class RightLineVisibilityConverterTestcs
    {
        private RightLineVisibilityConverter rightLineVisibilityConverter = new RightLineVisibilityConverter();

        /// <summary>
        /// Tests that an ArgumentException is thrown when the number of values is not 2
        /// </summary>
        [TestMethod]
        public void TestValuesIncorrectCount()
        {
            object[] values = new object[] { null };
            var exception = Assert.ThrowsException<ArgumentException>(() => rightLineVisibilityConverter.Convert(values, null, null, null));
            Assert.AreEqual("Incorrect value count", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when values[0] is not a string
        /// </summary>
        [TestMethod]
        public void TestValuesMissingString()
        {
            object[] values = new object[] { null, new ObservableCollection<string>() };
            var exception = Assert.ThrowsException<ArgumentException>(() => rightLineVisibilityConverter.Convert(values, null, null, null));
            Assert.AreEqual("values[0] is not a string", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when values[1] is not an ObservableCollection<string>
        /// </summary>
        [TestMethod]
        public void TestValuesMissingObservableCollection()
        {
            object[] values = new object[] { "", null };
            var exception = Assert.ThrowsException<ArgumentException>(() => rightLineVisibilityConverter.Convert(values, null, null, null));
            Assert.AreEqual("values[1] is not an ObservableCollection<string>", exception.Message);
        }

        /// <summary>
        /// Tests that the visibility is returned as hidden when the step is the last one in the collection
        /// </summary>
        [TestMethod]
        public void TestVisibilityHiddenWhenLastStep()
        {
            object[] values = new object[] { "lastStep", new ObservableCollection<string>() { "firstStep", "lastStep" } };
            Assert.AreEqual(Visibility.Hidden, rightLineVisibilityConverter.Convert(values, null, null, null));
        }

        /// <summary>
        /// Tests that the visibility is returned as visible when the step is not the last one in the collection
        /// </summary>
        [TestMethod]
        public void TestVisibilityVisibleWhenNotLastStep()
        {
            object[] values = new object[] { "firstStep", new ObservableCollection<string>() { "firstStep", "lastStep" } };
            Assert.AreEqual(Visibility.Visible, rightLineVisibilityConverter.Convert(values, null, null, null));
        }
    }
}
