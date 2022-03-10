using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.ECB.Builders.ECBUrlBuilder.Exceptions
{
    public class NoSpecifySeriesVariationException : Exception
    {
        public NoSpecifySeriesVariationException() : base() { }
        public NoSpecifySeriesVariationException(string message) : base(message) { }
        public NoSpecifySeriesVariationException(string message, Exception e) : base(message, e) { }
    }
}
