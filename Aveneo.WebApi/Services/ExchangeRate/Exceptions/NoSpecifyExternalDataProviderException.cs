using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.Exceptions
{
    public class NoSpecifyExternalDataProviderException : Exception
    {
        public NoSpecifyExternalDataProviderException() : base() { }
        public NoSpecifyExternalDataProviderException(string message) : base(message) { }
        public NoSpecifyExternalDataProviderException(string message, Exception e) : base(message, e) { }
    }
}
