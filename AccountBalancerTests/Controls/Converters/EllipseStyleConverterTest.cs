using AccountBalancer.Controls.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AccountBalancerTests.Controls.Converters
{
    [TestClass]
    public class EllipseStyleConverterTest
    {
        private EllipseStyleConverter ellipseStyleConverter = new EllipseStyleConverter();

        /// <summary>
        /// Tests that an ArgumentException is thrown when the number of values is not 4
        /// </summary>
        [TestMethod]
        public void TestValuesIncorrectCount()
        {
            object[] values = new object[] { null, null, null };
            var exception = Assert.ThrowsException<ArgumentException>(() => ellipseStyleConverter.Convert(values, null, null, null));
            Assert.AreEqual("Incorrect value count", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when values[0] is not an int
        /// </summary>
        [TestMethod]
        public void TestValuesMissingInt()
        {
            object[] values = new object[] { null, new ObservableCollection<string>(), "", new Ellipse() };
            var exception = Assert.ThrowsException<ArgumentException>(() => ellipseStyleConverter.Convert(values, null, null, null));
            Assert.AreEqual("values[0] is not an int", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when values[1] is not an ObservableCollection
        /// </summary>
        [TestMethod]
        public void TestValuesMissingObservableCollection()
        {
            object[] values = new object[] { 1, null, "", new Ellipse() };
            var exception = Assert.ThrowsException<ArgumentException>(() => ellipseStyleConverter.Convert(values, null, null, null));
            Assert.AreEqual("values[1] is not an ObservableCollection<string>", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when values[2] is not a string
        /// </summary>
        [TestMethod]
        public void TestValuesMissingString()
        {
            object[] values = new object[] { 1, new ObservableCollection<string>(), null, new Ellipse() };
            var exception = Assert.ThrowsException<ArgumentException>(() => ellipseStyleConverter.Convert(values, null, null, null));
            Assert.AreEqual("values[2] is not a string", exception.Message);
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown when values[3] is not an Ellipse
        /// </summary>
        [TestMethod]
        public void TestValuesMissingEllipse()
        {
            object[] values = new object[] { 1, new ObservableCollection<string>(), "", null };
            var exception = Assert.ThrowsException<ArgumentException>(() => ellipseStyleConverter.Convert(values, null, null, null));
            Assert.AreEqual("values[3] is not an Ellipse", exception.Message);
        }

        /// <summary>
        /// Tests that the Ellipse is a solid green fill with no stroke and the default stroke thickness (1) if the step is done
        /// </summary>
        [TestMethod]
        public void TestEllipseStyleStepDone()
        {
            ObservableCollection<string> collection = new ObservableCollection<string>() { "str0", "str1", "str2" };
            Ellipse ellipse = new Ellipse();

            object[] values = new object[] { 1, collection, "str0" , ellipse };
            ellipseStyleConverter.Convert(values, null, null, null);

            Assert.AreEqual(Colors.Green, (ellipse.Fill as SolidColorBrush).Color);
            Assert.AreEqual(null, ellipse.Stroke);
            Assert.AreEqual(1, ellipse.StrokeThickness);
        }

        /// <summary>
        /// Tests that the Ellipse has a white fill with a green stroke of thickness 3 if the step is in progress
        /// </summary>
        [TestMethod]
        public void TestEllipseStyleStepInProgress()
        {
            ObservableCollection<string> collection = new ObservableCollection<string>() { "str0", "str1", "str2" };
            Ellipse ellipse = new Ellipse();

            object[] values = new object[] { 1, collection, "str1", ellipse };
            ellipseStyleConverter.Convert(values, null, null, null);

            Assert.AreEqual(Colors.White, (ellipse.Fill as SolidColorBrush).Color);
            Assert.AreEqual(Colors.Green, (ellipse.Stroke as SolidColorBrush).Color);
            Assert.AreEqual(3, ellipse.StrokeThickness);
        }

        /// <summary>
        /// Tests that the Ellipse has a white fill with a grey stroke of thickness 3 if the step is no done
        /// </summary>
        [TestMethod]
        public void TestEllipseStyleStepNotDone()
        {
            ObservableCollection<string> collection = new ObservableCollection<string>() { "str0", "str1", "str2" };
            Ellipse ellipse = new Ellipse();

            object[] values = new object[] { 1, collection, "str2", ellipse };
            ellipseStyleConverter.Convert(values, null, null, null);

            Assert.AreEqual(Colors.White, (ellipse.Fill as SolidColorBrush).Color);
            Assert.AreEqual(Colors.Gray, (ellipse.Stroke as SolidColorBrush).Color);
            Assert.AreEqual(3, ellipse.StrokeThickness);
        }
    }
}
