using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistance.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan timeTolive = default)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket,
                (timeTolive == default) ? TimeSpan.FromDays(7) : timeTolive);
            if (IsCreatedOrUpdated)
            {
                //var Basket = await _database.StringGetAsync(basket.Id);
                //return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
                // = 
                return await GetBasketAsync(basket.Id);

            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(string basketid) => await _database.KeyDeleteAsync(basketid);


        public async Task<CustomerBasket?> GetBasketAsync(string basketid)
        {
            var Basket = await _database.StringGetAsync(basketid);
            if(Basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket?>(Basket!);
        }
    }
}
