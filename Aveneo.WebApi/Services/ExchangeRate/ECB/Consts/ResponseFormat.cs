using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.ECB.Consts
{
    public static class ResponseFormat
    {
        /// <summary>
        /// Comma-separated values
        /// </summary>
        public const String Csv = "csvdata";
        /// <summary>
        /// JSON
        /// </summary>
        public const String Json = "jsondata";
        /// <summary>
        /// SDMX-ML 2.1 Structure Specific data format
        /// </summary>
        public const String Specific = "structurespecificdata";
        /// <summary>
        /// SDMX-ML 2.1 Generic Data
        /// </summary>
        public const String Generic = "genericdata";

    }
}
