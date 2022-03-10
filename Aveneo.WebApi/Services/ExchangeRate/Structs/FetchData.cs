using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.Structs
{
    public struct FetchData
    {
        public FechType Type { get; set; }
        public String Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
