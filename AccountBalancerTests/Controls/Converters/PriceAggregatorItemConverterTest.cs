using AccountBalancer.Controls.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AccountBalancerTests.Controls.Converters
{
    [TestClass]
    public class PriceAggregatorItemConverterTest
    {
        private PriceAggregatorItemConverter priceAggregatorItemConverter = new PriceAggregatorItemConverter();

        /// <summary>
        /// Tests that an ArgumentException is thrown when the value is not a Tuple<decimal,int>
        /// </summary>
        [TestMethod]
        public void TestValueMissingTuple()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => priceAggregatorItemConverter.Convert(null, null, null, null));
            Assert.AreEqual("value is not a tuple<decimal,int>", exception.Message);
        }

        /// <summary>
        /// Tests that the decimal item of the supplied Tuple was returned
        /// </summary>
        [TestMethod]
        public void TestDecimalTupleItemReturned()
        {
            Tuple<decimal, int> tuple = new Tuple<decimal, int>(15.00M, 0);
            Assert.AreEqual(tuple.Item1, priceAggregatorItemConverter.Convert(tuple, null, null, null));
        }
    }
}
