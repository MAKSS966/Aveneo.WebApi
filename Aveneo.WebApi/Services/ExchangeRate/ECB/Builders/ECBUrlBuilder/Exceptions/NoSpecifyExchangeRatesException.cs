using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.ECB.Builders.ECBUrlBuilder.Exceptions
{
    public class NoSpecifyExchangeRatesException : Exception
    {
        public NoSpecifyExchangeRatesException() : base() { }
        public NoSpecifyExchangeRatesException(string message) : base(message) { }
        public NoSpecifyExchangeRatesException(string message, Exception e) : base(message, e) { }
    }
}
