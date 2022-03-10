using Aveneo.WebApi.Data;
using Aveneo.WebApi.Services.ExchangeRate.Builders.ECB.ECBUrlBuilder;
using Aveneo.WebApi.Services.ExchangeRate.Builders.ECB.ECBUrlBuilder.Interfaces;
using Aveneo.WebApi.Services.ExchangeRate.ECB.Consts;
using Aveneo.WebApi.Services.ExchangeRate.Structs;
using Aveneo.WebApi.Services.ExchangeRate.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Nager.Date;
using System.Security.Cryptography;
using Aveneo.WebApi.Services.ExchangeRate.Helpers;
using Aveneo.WebApi.Services.ExchangeRate.Cache.Interface;

namespace Aveneo.WebApi.Services.ExchangeRate.ECB
{
    public class EuropeanCentralBankProvider : IExternalRateProvider
    {
        private const String wsEntryPoint = "https://sdw-wsrest.ecb.europa.eu/service/";
        private const String flowRef = "EXR";
        private const String frequencyMeasured = FrequencyMeasured.DailyBasis;
        private const String exchangeRates = ExchangeRates.ForeignExchangeReferenceRates;
        private const String seriesVariation = SeriesVariation.AverageOrStandardise;
        private const String format = ResponseFormat.Json;



        private HttpClient _client = null;

        private IEuropeanCentralBankUrlBuilder UrlBuilder = null;
        private ICache _cache = null;
        public EuropeanCentralBankProvider(ICache cache)
        {
            this._cache = cache;
            this._client = new HttpClient();

        }
        private void PrepareRemoteURLs(DateTime startDate, DateTime endDate)
        {
            List<String> RemoteUrls = new List<String>();
            this.UrlBuilder = new EuropeanCentralBankUrlBuilder();
            this.UrlBuilder.AddTypeExchangeRates(exchangeRates);
            this.UrlBuilder.AddSeriesVariation(seriesVariation);
            this.UrlBuilder.AddFrequency(frequencyMeasured);
            this.UrlBuilder.AddFlowRef(flowRef);
            this.UrlBuilder.AddFormat(format);
            this.UrlBuilder.AddEndPeriod(endDate);
            this.UrlBuilder.AddStartPeriod(this.GetDateBeforeHoliday(startDate));

        }


        private DateTime GetDateBeforeHoliday(DateTime dateTime)
        {
            while (DateSystem.IsPublicHoliday(dateTime, CountryCode.PL))
            {
                dateTime = dateTime.AddDays(-1);
            }

            return dateTime;
        }


        private String GetRemoteURL(String currentCurreny, String measuredCurrency)
        {
            this.UrlBuilder.AddCurrentCurrency(currentCurreny);
            this.UrlBuilder.AddMeasuredCurrency(measuredCurrency);
            return this.UrlBuilder.GetUrl();
        }

        private async Task<FetchData> GetRemoteData(String url)
        {
            FetchData item = new FetchData();
            String key = url.GetHash();

            if(await this._cache.IsExist(key))
            {
                item = await this._cache.GetData(key);
                return item;
            }


            HttpResponseMessage response = await _client.GetAsync(url);

            item.Type = FechType.Api;
            item.StatusCode = response.StatusCode;

            if (item.StatusCode == System.Net.HttpStatusCode.OK)
            {
                item.Data = await response.Content.ReadAsStringAsync();

                await this._cache.StoreData(key, item.Data);
            }

            return item;
        }

        public async Task<List<RemoteData>> Get(Dictionary<string, string> currencyCodes, DateTime startDate, DateTime endDate)
        {
            this.PrepareRemoteURLs(startDate, endDate);

            List<RemoteData> remoteDatas = new List<RemoteData>();

            foreach (var item in currencyCodes)
            {
                RemoteData element = new RemoteData();
                element.CurrentCurrency = item.Key;
                element.MeasuredCurrency = item.Value;
                element.RemoteUrl = this.GetRemoteURL(element.CurrentCurrency, element.MeasuredCurrency);
                System.Diagnostics.Debug.WriteLine(element.RemoteUrl);
                element.Data = await this.GetRemoteData(element.RemoteUrl);
                remoteDatas.Add(element);

            }
            return remoteDatas;
        }
    }
}
