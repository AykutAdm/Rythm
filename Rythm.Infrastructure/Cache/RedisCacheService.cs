using Rythm.Application.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rythm.Infrastructure.Cache
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _database;
        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<T> GetAsync<T>(string key)
        {
            // Redis'ten key'e göre veriyi al
            var value = await _database.StringGetAsync(key);

            // Veri yoksa null döndür
            if (value.IsNullOrEmpty) return default;

            // JSON string'i C# objesine çevir ve döndür
            return JsonSerializer.Deserialize<T>(value!);
        }

        public async Task RemoveAsync(string key)
        {
            // Redis'ten key'i sil
            await _database.KeyDeleteAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            // C# objesini JSON string'e çevir
            var jsonValue = JsonSerializer.Serialize(value);

            // Redis'e kaydet, süre verilmişse o süre sonra otomatik silinir
            await _database.StringSetAsync(key, jsonValue, expiration ?? TimeSpan.FromMinutes(10));
        }
    }
}
