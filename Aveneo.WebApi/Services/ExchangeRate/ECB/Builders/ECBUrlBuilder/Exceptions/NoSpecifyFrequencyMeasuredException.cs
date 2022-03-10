using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.ECB.Builders.ECBUrlBuilder.Exceptions
{
    public class NoSpecifyFrequencyMeasuredException : Exception
    {
        public NoSpecifyFrequencyMeasuredException() : base() { }
        public NoSpecifyFrequencyMeasuredException(string message) : base(message) { }
        public NoSpecifyFrequencyMeasuredException(string message, Exception e) : base(message, e) { }
    }
}
