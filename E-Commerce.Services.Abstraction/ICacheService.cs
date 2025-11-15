using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Abstraction
{
    public interface ICacheService 
    {
        Task<string?> GetAsync(string Cachekey);
        Task SetAsync(string Cachekey, object Cachevalue, TimeSpan TimeToLive);

    }
}
