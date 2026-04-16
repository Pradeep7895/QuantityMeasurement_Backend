using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Repository;
using QuantityMeasurementApp.Model.Entities;

namespace QuantityMeasurementApp.Tests.EntityTest.TestsUC17
{
    [TestClass]
    public class QuantityHistoryRepositoryTests
    {
        private AppDbContext _context;
        private QuantityHistoryRepository _repo;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;

            _context = new AppDbContext(options);
            _repo = new QuantityHistoryRepository(_context, null);
        }

        [TestMethod]
        public void AddRecord_ShouldSaveToDatabase()
        {
            var record = new QuantityHistoryRecord
            {
                Category = "Length",
                OperationType = "Add",
                FirstValue = 5,
                FirstUnit = "FEET"
            };

            _repo.AddRecord(record);

            Assert.AreEqual(1, _context.QuantityHistory.Count());
        }

        [TestMethod]
        public void GetAllRecords_ShouldReturnData()
        {
            _context.QuantityHistory.Add(new QuantityHistoryRecord
            {
                Category = "Length",
                OperationType = "Add"
            });

            _context.SaveChanges();

            var result = _repo.GetAllRecords();

            Assert.IsTrue(result.Count > 0);
        }
    }
}