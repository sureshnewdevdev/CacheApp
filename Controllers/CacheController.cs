using CacheApp;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CacheController : ControllerBase
{
    private readonly IConnectionMultiplexer _redis;

    public CacheController(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> GetCacheValue(string key)
    {
        ServiceClass serviceClass = new ServiceClass();
        serviceClass.InsertEmployeeData();
        var db = _redis.GetDatabase();
        var value = await db.StringGetAsync(key);

        if (value.IsNullOrEmpty)
        {
            return NotFound("Key not found in cache.");
        }

        return Ok(value.ToString());
    }

    [HttpPost]
    public async Task<IActionResult> SetCacheValue([FromBody] CacheData cacheData)
    {
        var db = _redis.GetDatabase();
        await db.StringSetAsync(cacheData.Key, JsonSerializer.Serialize(cacheData.Value), cacheData.Expiration);
      
        return Ok("Cache set successfully.");
    }
}

public class CacheData
{
    public string Key { get; set; }
    public object Value { get; set; }
    public TimeSpan? Expiration { get; set; }
}
