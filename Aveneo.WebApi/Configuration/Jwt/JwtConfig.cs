using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Configuration.Jwt
{
    public class JwtConfig
    {
        public String Secret { get; set; }
        public String Issuer { get; set; }
        public String Audience { get; set; }
    }
}
