using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CryptoManager.WebApi.Utils
{
    public class JwtFactory
    {
        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(WebUtil.JwtKeyName)),
                                             SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
