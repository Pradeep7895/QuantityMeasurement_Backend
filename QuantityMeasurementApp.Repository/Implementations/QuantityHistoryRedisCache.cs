using StackExchange.Redis;
using System.Text.Json;
using System.Linq;
using QuantityMeasurementApp.Model.Entities;

namespace QuantityMeasurementApp.Repository.Implementations
{
    public class QuantityHistoryRedisCache
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        
        private const string KEY_RECORD = "quantity:record:{0}";
        private const string KEY_ALL = "quantity:all";
        private const string KEY_CATEGORY = "quantity:category:{0}";
        private const string KEY_OPERATION = "quantity:operation:{0}";

        public QuantityHistoryRedisCache(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
            _db = _redis.GetDatabase();
        }

        public void SetRecord(QuantityHistoryRecord record)
        {
            string json = JsonSerializer.Serialize(record);
            
            // Store individual record
            string recordKey = string.Format(KEY_RECORD, record.Id);
            _db.StringSet(recordKey, json);
            
            // Add to sorted sets
            _db.SortedSetAdd(KEY_ALL, json, record.CreatedAt.Ticks);
            
            if (!string.IsNullOrEmpty(record.Category))
            {
                string categoryKey = string.Format(KEY_CATEGORY, record.Category);
                _db.SortedSetAdd(categoryKey, json, record.CreatedAt.Ticks);
            }
            
            if (!string.IsNullOrEmpty(record.OperationType))
            {
                string operationKey = string.Format(KEY_OPERATION, record.OperationType);
                _db.SortedSetAdd(operationKey, json, record.CreatedAt.Ticks);
            }
        }

        public QuantityHistoryRecord? GetRecord(int id)
        {
            string key = string.Format(KEY_RECORD, id);
            string? json = _db.StringGet(key);
            
            if (string.IsNullOrEmpty(json))
                return null;
                
            return JsonSerializer.Deserialize<QuantityHistoryRecord>(json);
        }

        public List<QuantityHistoryRecord> GetAllRecords()
        {
            var records = new List<QuantityHistoryRecord>();
            var values = _db.SortedSetRangeByRank(KEY_ALL, 0, -1, Order.Descending);
            
            foreach (var value in values)
            {
                var record = JsonSerializer.Deserialize<QuantityHistoryRecord>(value.ToString());
                if (record != null) 
                    records.Add(record);
            }
            return records;
        }

        public List<QuantityHistoryRecord> GetRecordsByCategory(string category)
        {
            var records = new List<QuantityHistoryRecord>();
            string categoryKey = string.Format(KEY_CATEGORY, category);
            var values = _db.SortedSetRangeByRank(categoryKey, 0, -1, Order.Descending);
            
            foreach (var value in values)
            {
                var record = JsonSerializer.Deserialize<QuantityHistoryRecord>(value.ToString());
                if (record != null) 
                    records.Add(record);
            }
            return records;
        }

        public List<QuantityHistoryRecord> GetRecordsByOperationType(string operationType)
        {
            var records = new List<QuantityHistoryRecord>();
            string operationKey = string.Format(KEY_OPERATION, operationType);
            var values = _db.SortedSetRangeByRank(operationKey, 0, -1, Order.Descending);
            
            foreach (var value in values)
            {
                var record = JsonSerializer.Deserialize<QuantityHistoryRecord>(value.ToString());
                if (record != null) 
                    records.Add(record);
            }
            return records;
        }

        public void RemoveRecord(QuantityHistoryRecord record)
        {
            string recordKey = string.Format(KEY_RECORD, record.Id);
            _db.KeyDelete(recordKey);
            
            string json = JsonSerializer.Serialize(record);
            _db.SortedSetRemove(KEY_ALL, json);
            
            if (!string.IsNullOrEmpty(record.Category))
            {
                string categoryKey = string.Format(KEY_CATEGORY, record.Category);
                _db.SortedSetRemove(categoryKey, json);
            }
            
            if (!string.IsNullOrEmpty(record.OperationType))
            {
                string operationKey = string.Format(KEY_OPERATION, record.OperationType);
                _db.SortedSetRemove(operationKey, json);
            }
        }

        public void ClearAll()
        {
            var endpoints = _redis.GetEndPoints();
            var server = _redis.GetServer(endpoints.First());
            var keys = server.Keys(pattern: "quantity:*");
            
            foreach (var key in keys)
            {
                _db.KeyDelete(key);
            }
        }

        public void Dispose()
        {
            _redis?.Dispose();
        }
    }
}