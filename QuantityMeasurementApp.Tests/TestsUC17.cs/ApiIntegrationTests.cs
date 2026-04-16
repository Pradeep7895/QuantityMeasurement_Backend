using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace QuantityMeasurementApp.Tests.TestsUC17
{
    [TestClass]
    public class ApiIntegrationTests
    {
        private static WebApplicationFactory<Program> _factory;
        private static HttpClient _client;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [TestMethod]
        public async Task GetHistory_ShouldReturnSuccess()
        {
            var response = await _client.GetAsync("/api/quantities/history");

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task AddQuantity_ShouldReturnSuccess()
        {
            var request = new
            {
                q1 = new { value = 5, unit = "FEET", measurementType = "Length" },
                q2 = new { value = 5, unit = "FEET", measurementType = "Length" }
            };

            var response = await _client.PostAsJsonAsync("/api/quantities/add", request);

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task AddQuantity_WithTargetUnit_ShouldReturnConvertedResult()
        {
            var request = new
            {
                q1 = new { value = 1, unit = "FEET", measurementType = "Length" },
                q2 = new { value = 12, unit = "INCH", measurementType = "Length" },
                targetUnit = "INCH"
            };

            var response = await _client.PostAsJsonAsync("/api/quantities/add", request);

            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}