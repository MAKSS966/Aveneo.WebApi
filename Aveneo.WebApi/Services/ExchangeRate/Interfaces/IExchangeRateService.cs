using Aveneo.WebApi.Services.ExchangeRate.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.Interfaces
{
    public interface IExchangeRateService
    {
        Task<List<RemoteData>> Get(Dictionary<String, String> currencyCodes, DateTime startDate, DateTime endDate);
        void SetProvider(ProviderType providerType);
    }
}
