using E_Commerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistance.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database;
        public CacheRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<string?> GetAsync(string Cachekey)
        {
           var CacheValue = await _database.StringGetAsync(Cachekey);
            return CacheValue.IsNullOrEmpty ? null : CacheValue.ToString();
        }

        public async Task SetAsync(string Cachekey, string Cachevalue, TimeSpan TimeToLive)
        {
            await _database.StringSetAsync(Cachekey, Cachevalue, TimeToLive);
        }
    }
}
