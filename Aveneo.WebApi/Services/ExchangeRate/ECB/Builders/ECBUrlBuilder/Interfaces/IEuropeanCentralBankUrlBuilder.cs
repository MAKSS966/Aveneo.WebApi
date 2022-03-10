using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.Builders.ECB.ECBUrlBuilder.Interfaces
{
    public interface IEuropeanCentralBankUrlBuilder
    {
        void AddFlowRef(String flowRef);
        void AddFrequency(String frequency);
        void AddCurrentCurrency(String currency);
        void AddMeasuredCurrency(String currency);
        void AddTypeExchangeRates(String typeExchangeRates);
        void AddSeriesVariation(String seriesVariation);
        void AddStartPeriod(DateTime startPeriod);
        void AddEndPeriod(DateTime endPeriod);
        void AddFormat(String format);
        String GetUrl();
    }
}
