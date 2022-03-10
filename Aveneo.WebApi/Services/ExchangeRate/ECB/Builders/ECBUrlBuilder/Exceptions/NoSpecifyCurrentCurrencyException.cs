using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.ECB.Builders.ECBUrlBuilder.Exceptions
{
    public class NoSpecifyCurrentCurrencyException : Exception
    {
        public NoSpecifyCurrentCurrencyException() : base() { }
        public NoSpecifyCurrentCurrencyException(string message) : base(message) { }
        public NoSpecifyCurrentCurrencyException(string message, Exception e) : base(message, e) { }
    }
}
