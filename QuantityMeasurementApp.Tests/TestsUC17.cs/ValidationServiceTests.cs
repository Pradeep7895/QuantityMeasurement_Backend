using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Service.Services;
using QuantityMeasurementApp.Model.DTOs;

namespace QuantityMeasurementApp.Tests.EntityTest.TestsUC17
{
    [TestClass]
    public class ValidationServiceTests
    {
        private ValidationService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new ValidationService();
        }

        [TestMethod]
        public void SameMeasurement_ShouldReturnTrue()
        {
            var result = _service.CanPerformArithmetic(
                new QuantityDTO { MeasurementType = "Length" },
                new QuantityDTO { MeasurementType = "Length" });

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DifferentMeasurement_ShouldReturnFalse()
        {
            var result = _service.CanPerformArithmetic(
                new QuantityDTO { MeasurementType = "Length" },
                new QuantityDTO { MeasurementType = "Temperature" });

            Assert.IsFalse(result);
        }
    }
}