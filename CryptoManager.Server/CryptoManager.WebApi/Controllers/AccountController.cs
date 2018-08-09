using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CryptoManager.Domain.Entities;
using CryptoManager.Domain.IntegrationEntities.Facebook;
using CryptoManager.WebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CryptoManager.WebApi.Controllers
{
    /// <summary>
    /// Responsible to authenticated user and protect all API Controllers
    /// </summary>
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : BaseController
    {
        private const string FACEBOOK_PROVIDER_NAME = "Facebook";
        private readonly JwtFactory _jwtFactory;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private static readonly HttpClient _client = new HttpClient();

        public AccountController(JwtFactory jwtFactory,
                                 SignInManager<ApplicationUser> signInManager,
                                 UserManager<ApplicationUser> userManager,
                                 IMapper mapper) : base(mapper)
        {
            _jwtFactory = jwtFactory;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <summary>
        /// get user logged information
        /// </summary>
        /// <response code="400">If the user not logged</response>
        [HttpGet]
        [ProducesResponseType(typeof(ObjectResult), 400)]
        public async Task<IActionResult> GetUserInfo()
        {
            if (User.Identity.IsAuthenticated)
            {
                return new ObjectResult(await _userManager.FindByIdAsync(GetUserId().ToString()));
            }
            return BadRequest();
        }

        /// <summary>
        /// method to authenticate user in facebook and return a jwktoken with your data passing accesstoken from facebook
        /// </summary>
        /// <param name="accessToken">token that identify user in facebook</param>
        /// <returns>JWToken if User is successfully authenticated</returns>
        /// <response code="400">If the user not authenticated in facebook or if occurred other error</response>
        /// <response code="200">If success</response>
        [HttpPost]
        [AllowAnonymous]
        [Route("ExternalLoginFacebook")]
        [ProducesResponseType(typeof(ObjectResult), 400)]
        [ProducesResponseType(typeof(ObjectResult), 200)]
        public async Task<IActionResult> ExternalLoginFacebook(string accessToken)
        {
            try
            {
                // 1.generate an app access token
                var appAccessTokenResponse = await _client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={WebUtil.FacebookAppId}&client_secret={WebUtil.FacebookAppSecret}&grant_type=client_credentials");
                var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
                // 2. validate the user access token
                var userAccessTokenValidationResponse = await _client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={accessToken}&access_token={appAccessToken.AccessToken}");
                var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

                if (!userAccessTokenValidation.Data.IsValid)
                {
                    return new BadRequestObjectResult("login_failure - message:Invalid facebook token.");
                }

                // 3. we've got a valid token so we can request user data from fb
                var userInfoResponse = await _client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={accessToken}");
                var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

                // 4. ready to create the local user account (if necessary) and jwt
                var user = await _userManager.FindByEmailAsync(userInfo.Email);

                if (user == null)
                {
                    var appUser = new ApplicationUser
                    {
                        FirstName = userInfo.FirstName,
                        LastName = userInfo.LastName,
                        FacebookId = userInfo.Id,
                        Email = userInfo.Email,
                        UserName = userInfo.Email,
                        Gender = userInfo.Gender,
                        Locale = userInfo.Locale,
                        PictureUrl = userInfo.Picture.Data.Url
                    };

                    var result = await _userManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));                    
                    if (!result.Succeeded)
                        return new BadRequestObjectResult(result);

                    result = await AddSuperUserInRoleAdmin(appUser);
                    if (result != null && !result.Succeeded)
                        return new BadRequestObjectResult(result);
                }
                else
                {
                    user.FirstName = userInfo.FirstName;
                    user.LastName = userInfo.LastName;
                    user.FacebookId = userInfo.Id;
                    user.Email = userInfo.Email;
                    user.UserName = userInfo.Email;
                    user.Gender = userInfo.Gender;
                    user.Locale = userInfo.Locale;
                    user.PictureUrl = userInfo.Picture.Data.Url;

                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                        return new BadRequestObjectResult(result);

                    result = await AddSuperUserInRoleAdmin(user);
                    if (result != null && !result.Succeeded)
                        return new BadRequestObjectResult(result);
                }

                var localUser = await _userManager.FindByEmailAsync(userInfo.Email);

                if (localUser == null)
                {
                    return new BadRequestObjectResult("login_failure - message:Failed to create local user account.");
                }
                await _signInManager.SignInAsync(localUser, true, "Bearer");
                return new OkObjectResult(await GenerateToken(localUser));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        private async Task<IdentityResult> AddSuperUserInRoleAdmin(ApplicationUser user)
        {
            if (user.Email.Equals(WebUtil.SuperUserEmail))
            {
                if (!await _userManager.IsInRoleAsync(user, WebUtil.ADMINISTRATOR_ROLE_NAME))
                {
                    return await _userManager.AddToRoleAsync(user, WebUtil.ADMINISTRATOR_ROLE_NAME);
                }
            }
            return null;
        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", $"{user.FirstName} {user.LastName}"),
                new Claim("Email", user.Email),
                new Claim("PictureURL", user.PictureUrl),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var roleNames = await _userManager.GetRolesAsync(user);
            foreach (var roleName in roleNames)
            {
                var roleClaim = new Claim(ClaimTypes.Role, roleName);
                claims.Add(roleClaim);
            }

            return _jwtFactory.GenerateToken(claims);
        }
    }
}