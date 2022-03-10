using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.Token.Interface
{
    public interface ITokenService
    {
        String GetToken(String key, String issuer);
        bool ValidateToken(String key, String issuer, String audience, String token);
    }
}
