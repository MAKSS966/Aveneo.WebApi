using Aveneo.WebApi.Services.ExchangeRate.Structs;
using Aveneo.WebApi.Services.ExchangeRate.Exceptions;
using Aveneo.WebApi.Services.ExchangeRate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aveneo.WebApi.Data;
using Microsoft.Extensions.Caching.Memory;
using Aveneo.WebApi.Services.ExchangeRate.ECB;
using Aveneo.WebApi.Services.ExchangeRate.Cache.Interface;
using Microsoft.Extensions.Caching.Distributed;
using Aveneo.WebApi.Services.ExchangeRate.Cache;

namespace Aveneo.WebApi.Services.ExchangeRate
{
    public class ExchangeRateService : IExchangeRateService
    {
        private IExternalRateProvider ExternalData = null;
        private readonly AveneoContext _context;
        private readonly IDistributedCache _memoryCache;
        private ICache _cache;
        public ExchangeRateService(AveneoContext context, IDistributedCache memoryCache)
        {
            this._context = context;
            this._memoryCache = memoryCache;
            this._cache = new Cached(this._context, this._memoryCache);
        }
        public Task<List<RemoteData>> Get(Dictionary<string, string> currencyCodes, DateTime startDate, DateTime endDate)
        {
            if (this.ExternalData == null)
                throw new NoSpecifyExternalDataProviderException();

            return this.ExternalData.Get(currencyCodes, startDate, endDate);
        }

        public void SetProvider(ProviderType providerType)
        {
            switch(providerType)
            {
                case ProviderType.EuropeanCentralBank:
                    {
                        this.ExternalData = new EuropeanCentralBankProvider(this._cache);
                    }
                    break;
                default:
                    {
                        throw new UnknowProviderTypeException();
                    }
            }
        }
    }
}
