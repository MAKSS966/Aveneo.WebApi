using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.ECB.Builders.ECBUrlBuilder.Exceptions
{
    public class UnknowFrequencyMeasuredException : Exception
    {
        public UnknowFrequencyMeasuredException() : base() { }
        public UnknowFrequencyMeasuredException(string message) : base(message) { }
        public UnknowFrequencyMeasuredException(string message, Exception e) : base(message, e) { }
    }
}
