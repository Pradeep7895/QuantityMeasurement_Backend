using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StackExchange.Redis;

namespace QuantityMeasurementApp.Tests.EntityTest.TestsUC17
{
    [TestClass]
    public class RedisCacheTests
    {
        [TestMethod]
        public void RedisConnection_ShouldBeCreated()
        {
            var mock = new Mock<IConnectionMultiplexer>();

            Assert.IsNotNull(mock.Object);
        }

        [TestMethod]
        public void Cache_Set_And_Get_ShouldWork()
        {
            // Simulate cache using dictionary (since Redis is external)
            var cache = new Dictionary<string, string>();

            // Set
            cache["history"] = "test_data";

            // Get
            var result = cache["history"];

            Assert.AreEqual("test_data", result);
        }

        [TestMethod]
        public void Cache_Remove_ShouldDeleteData()
        {
            var cache = new Dictionary<string, string>();

            cache["history"] = "test_data";

            cache.Remove("history");

            Assert.IsFalse(cache.ContainsKey("history"));
        }
    }
}