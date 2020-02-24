using AccountBalancer.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AccountBalancerTests.Converters
{
    [TestClass]
    public class StatusTextBlockConverterTestcs
    {
        private StatusTextBlockConverter statusTextBlockConverter = new StatusTextBlockConverter();

        /// <summary>
        /// Tests that null is returned in the case that the values are unset from their dependency property binding
        /// </summary>
        [TestMethod]
        public void TestValuesDependencyPropertyUnsetValue()
        {
            object[] values = new object[] { DependencyProperty.UnsetValue };
            Assert.IsNull(statusTextBlockConverter.Convert(values, null, null, null));
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when the number of values is not 3
        /// </summary>
        [TestMethod]
        public void TestValuesIncorrectCount()
        {
            object[] values = new object[] { null, null };
            var exception = Assert.ThrowsException<ArgumentException>(() => statusTextBlockConverter.Convert(values, null, null, null));
            Assert.AreEqual("Incorrect value count", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when values[0] is not a decimal
        /// </summary>
        [TestMethod]
        public void TestValuesMissingFirstDecimal()
        {
            object[] values = new object[] { null, 50.00M, new TextBlock() };
            var exception = Assert.ThrowsException<ArgumentException>(() => statusTextBlockConverter.Convert(values, null, null, null));
            Assert.AreEqual("values[0] is not a decimal", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when values[1] is not a decimal
        /// </summary>
        [TestMethod]
        public void TestValuesMissingSecondDecimal()
        {
            object[] values = new object[] { 50.00M, null, new TextBlock() };
            var exception = Assert.ThrowsException<ArgumentException>(() => statusTextBlockConverter.Convert(values, null, null, null));
            Assert.AreEqual("values[1] is not a decimal", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when values[2] is not a TextBlock
        /// </summary>
        [TestMethod]
        public void TestValuesMissingTextBlock()
        {
            object[] values = new object[] { 5.00M, 5.00M, null };
            var exception = Assert.ThrowsException<ArgumentException>(() => statusTextBlockConverter.Convert(values, null, null, null));
            Assert.AreEqual("values[2] is not a TextBlock", exception.Message);
        }

        /// <summary>
        /// Tests that the TextBlock text is set and is colored green when the total matches the new account register balance
        /// </summary>
        [TestMethod]
        public void TestTotalMatchesAccountRegisterBalance()
        {
            TextBlock textBlock = new TextBlock();
            object[] values = new object[] { 5.00M, 5.00M, textBlock };
            statusTextBlockConverter.Convert(values, null, null, null);

            Assert.AreEqual("Total matches account register balance", textBlock.Text);
            Assert.AreEqual(Colors.Green, ((SolidColorBrush)textBlock.Foreground).Color);
        }

        /// <summary>
        /// Tests that the TextBlock text is set and is colored red when the total does not match the new account register balance
        /// </summary>
        [TestMethod]
        public void TestTotalDoesNotMatchAccountRegisterBalance()
        {
            TextBlock textBlock = new TextBlock();
            object[] values = new object[] { 5.00M, 3.00M, textBlock };
            statusTextBlockConverter.Convert(values, null, null, null);

            Assert.AreEqual("Total does NOT match account register balance. Difference of $2.00", textBlock.Text);
            Assert.AreEqual(Colors.Red, ((SolidColorBrush)textBlock.Foreground).Color);
        }
    }
}
