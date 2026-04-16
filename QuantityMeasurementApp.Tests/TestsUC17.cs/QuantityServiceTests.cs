// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Moq;
// using QuantityMeasurementApp.Service.Services;
// using QuantityMeasurementApp.Service.Interfaces;
// using QuantityMeasurementApp.Model.DTOs;
// using QuantityMeasurementApp.Repository.Interfaces;

// namespace QuantityMeasurementApp.Tests.EntityTest.TestsUC17
// {
//     [TestClass]
//     public class QuantityServiceTests
//     {
//         private Mock<IArithmeticService> _arith;
//         private Mock<IConversionService> _conv;
//         private Mock<IValidationService> _val;
//         private Mock<IEqualityService> _equal;
//         private Mock<IQuantityHistoryRepository> _repo;

//         private QuantityService _service;

//         [TestInitialize]
//         public void Setup()
//         {
//             _arith = new Mock<IArithmeticService>();
//             _conv = new Mock<IConversionService>();
//             _val = new Mock<IValidationService>();
//             _equal = new Mock<IEqualityService>();
//             _repo = new Mock<IQuantityHistoryRepository>();

//             _val.Setup(x => x.CanPerformArithmetic(It.IsAny<QuantityDTO>(), It.IsAny<QuantityDTO>()))
//                 .Returns(true);

//             _service = new QuantityService(
//                 _conv.Object,
//                 _arith.Object,
//                 _equal.Object,
//                 _val.Object,
//                 _repo.Object
//             );
//         }

//         [TestMethod]
//         public void Add_ValidInput_ReturnsResult()
//         {
//             var q1 = new QuantityDTO { Value = 5, Unit = "FEET" };
//             var q2 = new QuantityDTO { Value = 5, Unit = "FEET" };

//             _arith.Setup(a => a.Add(q1, q2))
//                   .Returns(new QuantityDTO { Value = 10, Unit = "FEET" });

//             var result = _service.Add(q1, q2);

//             Assert.AreEqual(10, result.Value);
//         }

//         [TestMethod]
//         public void Add_WithTargetUnit_ShouldConvert()
//         {
//             var baseResult = new QuantityDTO { Value = 2, Unit = "FEET" };
//             var converted = new QuantityDTO { Value = 24, Unit = "INCH" };

//             _arith.Setup(a => a.Add(It.IsAny<QuantityDTO>(), It.IsAny<QuantityDTO>()))
//                   .Returns(baseResult);

//             _conv.Setup(c => c.Convert(baseResult, "INCH"))
//                  .Returns(converted);

//             var result = _service.Add(new QuantityDTO(), new QuantityDTO(), "INCH");

//             Assert.AreEqual("INCH", result.Unit);
//         }
//     }
// }