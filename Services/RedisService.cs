using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using System.Security;
using System.Threading.Tasks;

namespace DevKickstart.Api.Services;

public class RedisService
    {
        private readonly IDatabase _db;

        public RedisService(IConfiguration config)
        {
            var connection = config["Redis:ConnectionString"];
            var redis = ConnectionMultiplexer.Connect(connection);
            _db = redis.GetDatabase();
        }

        public async Task SetValue(string key, string value)
        {
          await _db.StringSetAsync(key, value);
        }

        public async Task<string?> GetValue(string key)
        {
          return await _db.StringGetAsync(key);
        }
        
    }