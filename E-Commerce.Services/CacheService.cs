using E_Commerce.Domain.Contracts;
using E_Commerce.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            this._cacheRepository = cacheRepository;
        }

        public async Task<string?> GetAsync(string Cachekey)
        {
            return await _cacheRepository.GetAsync(Cachekey);
        }

        public async Task SetAsync(string Cachekey, object Cachevalue, TimeSpan TimeToLive)
        {
            var Value = JsonSerializer.Serialize(Cachevalue,new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await _cacheRepository.SetAsync(Cachekey, Value, TimeToLive);

        }
    }
}
