using Aveneo.WebApi.Data;
using Aveneo.WebApi.Services.ExchangeRate.Cache.Interface;
using Aveneo.WebApi.Services.ExchangeRate.Structs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.Cache.DB
{
    public class CacheDB : ICache
    {
        private readonly AveneoContext _context;
        public CacheDB(AveneoContext context)
        {
            this._context = context;
        }
        public async Task<FetchData> GetData(string key)
        {
            FetchData item = new FetchData();
            if (await this.IsExist(key))
            {
                item.Type = FechType.Db;
                item.Data = (await this._context.ExchangeRates.SingleAsync(x => x.Key.Equals(key))).Data;

                return item;
            }
            return item;
        }

        public async Task<bool> IsExist(string key)
        {
            return await this._context.ExchangeRates.AnyAsync(x => x.Key.Equals(key));
        }

        public async Task<bool> StoreData(string key, string data)
        {
            this._context.ExchangeRates.Add(new Model.DB.ExchangeRate()
            {
                Key = key,
                Data = data
            });
            int id = await this._context.SaveChangesAsync();

            if (id != 0)
                return true;

            return false;
        }
    }
}
