using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.ECB.Builders.ECBUrlBuilder.Exceptions
{
    public class NoSpecifyFlowRefException : Exception
    {
        public NoSpecifyFlowRefException() : base() { }
        public NoSpecifyFlowRefException(string message) : base(message) { }
        public NoSpecifyFlowRefException(string message, Exception e) : base(message, e) { }
    }
}
