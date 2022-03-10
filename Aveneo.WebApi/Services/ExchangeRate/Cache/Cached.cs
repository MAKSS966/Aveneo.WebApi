using Aveneo.WebApi.Data;
using Aveneo.WebApi.Services.ExchangeRate.Cache.DB;
using Aveneo.WebApi.Services.ExchangeRate.Cache.Interface;
using Aveneo.WebApi.Services.ExchangeRate.Cache.Memory;
using Aveneo.WebApi.Services.ExchangeRate.Structs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.Cache
{
    public class Cached : ICache
    {
        private ICache DB;
        private ICache memory;

        private readonly AveneoContext _context;
        private readonly IDistributedCache _memoryCache;

        public Cached(AveneoContext context, IDistributedCache memoryCache)
        {
            this._context = context;
            this._memoryCache = memoryCache;

            this.DB = new CacheDB(this._context);
            this.memory = new CacheMemory(this._memoryCache);

        }

        public async Task<FetchData> GetData(string key)
        {
            FetchData item = new FetchData();
            if (await this.memory.IsExist(key))
            {
                item = await this.memory.GetData(key);
            }
            else if (await this.DB.IsExist(key))
            {
                item = await this.DB.GetData(key);
                await this.memory.StoreData(key, item.Data);
            }

            return item;
        }

        public async Task<bool> IsExist(string key)
        {
            if (await this.memory.IsExist(key))
                return true;

            if (await this.DB.IsExist(key))
                return true;

            return false;
        }

        public async Task<bool> StoreData(string key, string data)
        {
            try
            {
                await this.memory.StoreData(key, data);
                await this.DB.StoreData(key, data);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
