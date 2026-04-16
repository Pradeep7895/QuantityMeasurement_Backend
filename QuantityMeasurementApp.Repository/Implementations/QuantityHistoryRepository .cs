using QuantityMeasurementApp.Repository.Interfaces;
using QuantityMeasurementApp.Model.Entities;
using Microsoft.EntityFrameworkCore;

public class QuantityHistoryRepository : IQuantityHistoryRepository
{
    private readonly AppDbContext _context;
    private readonly RedisCacheService _cache;

    public QuantityHistoryRepository(AppDbContext context, RedisCacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public void AddRecord(QuantityHistoryRecord record)
    {
        _context.QuantityHistory.Add(record);
        _context.SaveChanges();

        // CLEAR CACHE
        _cache.Remove("history");
    }

    public List<QuantityHistoryRecord> GetAllRecords()
    {
        string key = "history";

        // STEP 1: CHECK REDIS
        var cached = _cache.Get(key);

        if (!string.IsNullOrEmpty(cached))
        {
            Console.WriteLine("From Redis");
            return System.Text.Json.JsonSerializer
                .Deserialize<List<QuantityHistoryRecord>>(cached);
        }

        // STEP 2: GET FROM DB
        Console.WriteLine("From DB");
        var data = _context.QuantityHistory.ToList();

        // STEP 3: SAVE IN REDIS
        var json = System.Text.Json.JsonSerializer.Serialize(data);
        _cache.Set(key, json);

        return data;
    }

    public QuantityHistoryRecord? GetRecordById(int id)
    {
        return _context.QuantityHistory.Find(id);
    }

    public List<QuantityHistoryRecord> GetRecordsByCategory(string category)
    {
        return _context.QuantityHistory
            .Where(x => x.Category == category)
            .ToList();
    }

    public List<QuantityHistoryRecord> GetRecordsByOperationType(string operation)
    {
        return _context.QuantityHistory
            .Where(x => x.OperationType == operation)
            .ToList();
    }

    public bool DeleteRecord(int id)
    {
        var record = _context.QuantityHistory.Find(id);
        if (record == null) return false;

        _context.QuantityHistory.Remove(record);
        _context.SaveChanges();
        return true;
    }

    public int DeleteAllRecords()
    {
        var records = _context.QuantityHistory.ToList();
        _context.QuantityHistory.RemoveRange(records);
        return _context.SaveChanges();
    }

    public int GetRecordCount()
    {
        return _context.QuantityHistory.Count();
    }

    public void ClearCache() { }
    public void RefreshCache() { }

    public void Close() { }
}