using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CryptoManager.WebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoManager.WebApi.Controllers
{
    /// <summary>
    /// Responsible to generate a Token that used to protect all API Controllers
    /// </summary>
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : Controller
    {
        private readonly JwtFactory _jwtFactory;
        public TokenController(JwtFactory jwtFactory)
        {
            _jwtFactory = jwtFactory;
        }
        /// <summary>
        /// Used to validate a username and password and generate a Valid Token
        /// </summary>
        /// <param name="username">Username to validate</param>
        /// <param name="password">Password to validate</param>
        /// <returns>A valid token with claim of user validated</returns>
        /// <response code="400">If the user is not valid</response>   
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(ObjectResult), 400)]
        public IActionResult Create(string username, string password)
        {
            if (username==password)
                return new ObjectResult(GenerateToken(username));
            return BadRequest();
        }

        private string GenerateToken(string username)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            return _jwtFactory.GenerateToken(claims);
        }
    }
}