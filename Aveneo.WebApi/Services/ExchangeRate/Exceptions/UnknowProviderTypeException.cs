using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.Exceptions
{
    public class UnknowProviderTypeException : Exception
    {
        public UnknowProviderTypeException() : base() { }
        public UnknowProviderTypeException(string message) : base(message) { }
        public UnknowProviderTypeException(string message, Exception e) : base(message, e) { }
    }
}
