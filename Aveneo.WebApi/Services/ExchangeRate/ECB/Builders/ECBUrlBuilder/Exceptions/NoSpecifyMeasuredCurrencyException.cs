using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.ECB.Builders.ECBUrlBuilder.Exceptions
{
    public class NoSpecifyMeasuredCurrencyException : Exception
    {
        public NoSpecifyMeasuredCurrencyException() : base() { }
        public NoSpecifyMeasuredCurrencyException(string message) : base(message) { }
        public NoSpecifyMeasuredCurrencyException(string message, Exception e) : base(message, e) { }
    }
}
