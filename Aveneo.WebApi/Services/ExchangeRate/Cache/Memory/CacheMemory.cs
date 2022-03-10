using Aveneo.WebApi.Services.ExchangeRate.Cache.Interface;
using Aveneo.WebApi.Services.ExchangeRate.Structs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.Cache.Memory
{
    public class CacheMemory : ICache
    {
        public readonly IDistributedCache _memoryCache;
        public String Data = String.Empty;
        public CacheMemory(IDistributedCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }
        public async Task<FetchData> GetData(string key)
        {
            FetchData item = new FetchData();

            item.Type = FechType.Memory;
            item.Data = await this._memoryCache.GetStringAsync(key);

            return item;
        }

        public async Task<bool> IsExist(string key)
        {
            byte[] b = await this._memoryCache.GetAsync(key);

            if (b != null)
                return true;

            return false;
        }

        public async Task<bool> StoreData(string key, string data)
        {
            await this._memoryCache.SetStringAsync(key, data);

            return true;
        }
    }
}
