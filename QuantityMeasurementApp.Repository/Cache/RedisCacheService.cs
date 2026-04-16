using StackExchange.Redis;

public class RedisCacheService
{
    private readonly IDatabase _db;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public string? Get(string key)
    {
        return _db.StringGet(key);
    }

    public void Set(string key, string value)
    {
        _db.StringSet(key, value, TimeSpan.FromMinutes(30));
    }

    public void Remove(string key)
    {
        _db.KeyDelete(key);
    }
}