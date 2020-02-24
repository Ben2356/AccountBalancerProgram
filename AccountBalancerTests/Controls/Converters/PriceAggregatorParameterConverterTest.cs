using AccountBalancer.Controls.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AccountBalancerTests.Controls.Converters
{
    [TestClass]
    public class PriceAggregatorParameterConverterTest
    {
        /// <summary>
        /// Tests that a ArgumentNullException is thrown when the supplied values parameter is null
        /// </summary>
        [TestMethod]
        public void TestValuesNull()
        {
            PriceAggregatorParameterConverter priceAggregatorParameterConverter = new PriceAggregatorParameterConverter();
            var exception = Assert.ThrowsException<ArgumentNullException>(() => priceAggregatorParameterConverter.Convert(null, null, null, null));
            Assert.AreEqual("values", exception.ParamName);
        }
    }
}
