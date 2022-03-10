using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.Structs
{
    public struct RemoteData
    {
        public String CurrentCurrency { get; set; }
        public String MeasuredCurrency { get; set; }
        public String RemoteUrl { get; set; }
        public FetchData Data { get; set; }
    }
}
