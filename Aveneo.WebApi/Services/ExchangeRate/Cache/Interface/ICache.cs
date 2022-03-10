using Aveneo.WebApi.Services.ExchangeRate.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.Cache.Interface
{
    public interface ICache
    {
        public Task<Boolean> IsExist(String key);
        public Task<FetchData> GetData(String key);
        public Task<Boolean> StoreData(String key, String data);
    }
}
