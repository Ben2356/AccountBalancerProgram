using AccountBalancer.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AccountBalancerTests.Converters
{
    [TestClass]
    public class StatusImageSourceConverterTest
    {
        private StatusImageSourceConverter statusImageSourceConverter = new StatusImageSourceConverter();

        /// <summary>
        /// Tests that null is returned in the case that the values are unset from their dependency property binding
        /// </summary>
        [TestMethod]
        public void TestValuesDependencyPropertyUnsetValue()
        {
            object[] values = new object[] { DependencyProperty.UnsetValue };
            Assert.IsNull(statusImageSourceConverter.Convert(values, null, null, null));
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when the number of values is not 2
        /// </summary>
        [TestMethod]
        public void TestValuesIncorrectCount()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => statusImageSourceConverter.Convert(new object[] { null }, null, null, null));
            Assert.AreEqual("Incorrect value count", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when values[0] is not a decimal
        /// </summary>
        [TestMethod]
        public void TestValuesMissingFirstDecimal()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => statusImageSourceConverter.Convert(new object[] { null, 50.00M }, null, null, null));
            Assert.AreEqual("values[0] is not a decimal", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when values[1] is not a decimal
        /// </summary>
        [TestMethod]
        public void TestValuesMissingSecondDecimal()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => statusImageSourceConverter.Convert(new object[] { 50.00M, null }, null, null, null));
            Assert.AreEqual("values[1] is not a decimal", exception.Message);
        }

        /// <summary>
        /// Tests that the statusOk icon is returned when the total matches the new account register balance
        /// </summary>
        [TestMethod]
        public void TestStatusOkIcon()
        {
            BitmapImage statusOkIcon = new BitmapImage(new Uri(@"/Resources/StatusOK_16x.png", UriKind.Relative));
            BitmapImage result = (BitmapImage)statusImageSourceConverter.Convert(new object[] { 5.00M, 5.00M }, null, null, null);
            Assert.AreEqual(statusOkIcon.UriSource, result.UriSource);
        }

        /// <summary>
        /// Tests that the statusInvalid icon is returned when the total does not match the new account register balance
        /// </summary>
        [TestMethod]
        public void TestStatusInvalidIcon()
        {
            BitmapImage statusInvalidIcon = new BitmapImage(new Uri(@"/Resources/StatusInvalid_16x.png", UriKind.Relative));
            BitmapImage result = (BitmapImage)statusImageSourceConverter.Convert(new object[] { 5.00M, 2.00M }, null, null, null);
            Assert.AreEqual(statusInvalidIcon.UriSource, result.UriSource);
        }
    }
}
