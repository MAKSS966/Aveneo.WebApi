using Aveneo.WebApi.Services.ExchangeRate.Builders.ECB.ECBUrlBuilder.Interfaces;
using Aveneo.WebApi.Services.ExchangeRate.ECB.Builders.ECBUrlBuilder.Exceptions;
using Aveneo.WebApi.Services.ExchangeRate.ECB.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.Builders.ECB.ECBUrlBuilder
{
    public class EuropeanCentralBankUrlBuilder : IEuropeanCentralBankUrlBuilder
    {
        private UriBuilder _uriBuilder = null;
        private String _scheme = "https";
        private String _host = "sdw-wsrest.ecb.europa.eu";
        private String _service = "service";
        private String _resource = "data";
        private String _flowRef = String.Empty;
        private String _frequencyMeasured = String.Empty;
        private String _exchangeRates = String.Empty;
        private String _seriesVariation = String.Empty;
        private String _format = String.Empty;
        private String _startPeriod = String.Empty;
        private String _endPeriod = String.Empty;
        private String _currentCurrency = String.Empty;
        private String _measuredCurrency = String.Empty;
        private String _delimeter = "/";
        public EuropeanCentralBankUrlBuilder()
        {
            this._uriBuilder = new UriBuilder();
            this._uriBuilder.Scheme = this._scheme;
            this._uriBuilder.Host = this._host;
        }

        public void AddCurrentCurrency(string currency)
        {
            this._currentCurrency = currency;
        }

        public void AddEndPeriod(DateTime endPeriod)
        {
            if(String.IsNullOrEmpty(this._frequencyMeasured))
                throw new NoSpecifyFrequencyMeasuredException();

            this._endPeriod = this.ConvertDateTimeToString(endPeriod);
           
        }

        private String ConvertDateTimeToString(DateTime dateTime)
        {
            String str = String.Empty;
            switch (this._frequencyMeasured)
            {
                case FrequencyMeasured.DailyBasis:
                    {
                        str = dateTime.ToString("yyyy-MM-dd");
                    }
                    break; ;
                default: throw new UnknowFrequencyMeasuredException();
            }
            return str;
        }

        public void AddFlowRef(string flowRef)
        {
            this._flowRef = flowRef;
        }

        public void AddFormat(string format)
        {
            this._format = format;
        }

        public void AddFrequency(string frequency)
        {
            this._frequencyMeasured = frequency;
        }


        public void AddMeasuredCurrency(string currency)
        {
            this._measuredCurrency = currency;
        }


        public void AddSeriesVariation(string seriesVariation)
        {
            this._seriesVariation = seriesVariation;
        }

        public void AddStartPeriod(DateTime startPeriod)
        {
            if (String.IsNullOrEmpty(this._frequencyMeasured))
                throw new NoSpecifyFrequencyMeasuredException();

            this._startPeriod = this.ConvertDateTimeToString(startPeriod);
        }

        public void AddTypeExchangeRates(string typeExchangeRates)
        {
            this._exchangeRates = typeExchangeRates;
        }

        private void CheckConsistent()
        {
            if (String.IsNullOrEmpty(this._flowRef))
                throw new NoSpecifyFlowRefException();

            if (String.IsNullOrEmpty(this._frequencyMeasured))
                throw new NoSpecifyFrequencyMeasuredException();

            if (String.IsNullOrEmpty(this._currentCurrency))
                throw new NoSpecifyCurrentCurrencyException();

            if (String.IsNullOrEmpty(this._measuredCurrency))
                throw new NoSpecifyMeasuredCurrencyException();

            if (String.IsNullOrEmpty(this._exchangeRates))
                throw new NoSpecifyExchangeRatesException();

            if (String.IsNullOrEmpty(this._seriesVariation))
                throw new NoSpecifyExchangeRatesException();
        }

        private String GetAdditionlParameters()
        {
            StringBuilder additional = new StringBuilder();
            if (!String.IsNullOrEmpty(this._startPeriod))
            {
                additional.Append("startPeriod=");
                additional.Append(this._startPeriod);
                additional.Append("&");
            }

            if (!String.IsNullOrEmpty(this._endPeriod))
            {
                additional.Append("endPeriod=");
                additional.Append(this._endPeriod);
                additional.Append("&");
            }

            if (!String.IsNullOrEmpty(this._format))
            {
                additional.Append("format=");
                additional.Append(this._format);
                additional.Append("&");
            }
            String str = additional.ToString();

            if(str.Length > 1)
                str = str.Remove(str.Length - 1);

            return str;
        }

        public string GetUrl()
        {
            this.CheckConsistent();

            StringBuilder sb = new StringBuilder();
            sb.Append(this._service);
            sb.Append(this._delimeter);
            sb.Append(this._resource);
            sb.Append(this._delimeter);
            sb.Append(this._flowRef);
            sb.Append(this._delimeter);
            sb.Append(this._frequencyMeasured);
            sb.Append(".");
            sb.Append(this._currentCurrency);
            sb.Append(".");
            sb.Append(this._measuredCurrency);
            sb.Append(".");
            sb.Append(this._exchangeRates);
            sb.Append(".");
            sb.Append(this._seriesVariation);

            this._uriBuilder.Path = sb.ToString();
            this._uriBuilder.Query = this.GetAdditionlParameters();

            //String additional = this.GetAdditionlParameters();

            //if(!String.IsNullOrEmpty(additional))
            //{
            //    sb.Append("?");
            //    sb.Append(additional);
            //}





            return this._uriBuilder.ToString();

        }
    }
}
