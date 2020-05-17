using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Abp.Runtime.Session;
using Abp.Web.Models;
using LeesStore.Authentication.JwtBearer;
using LeesStore.Authorization;
using LeesStore.Authorization.Users;
using LeesStore.Models.TokenAuth;
using LeesStore.MultiTenancy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LeesStore.Controllers.Client.V1
{
    public class ClientAuthenticateModel
    {
        [Required]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string ApiKey { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        public string Secret { get; set; }
    }

    public class ClientTokenAuthController : LeesStoreControllerBase
    {
        private readonly IApiKeyAuthenticationService _apiKeyAuthenticationService;
        private readonly IAbpSession _session;
        private readonly ITenantCache _tenantCache;
        private readonly LogInManager _logInManager;
        private readonly TokenAuthConfiguration _configuration;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;

        public ClientTokenAuthController(
            IApiKeyAuthenticationService apiKeyAuthenticationService,
            IAbpSession session,
            ITenantCache tenantCache,
            LogInManager logInManager,
            TokenAuthConfiguration configuration,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper)
        {
            _apiKeyAuthenticationService = apiKeyAuthenticationService;
            _session = session;
            _tenantCache = tenantCache;
            _logInManager = logInManager;
            _configuration = configuration;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
        }

        /// <summary>
        /// Authenticates an API Key and Secret.  If successful AuthenticateResultModel will contain a token that
        /// should be passed to subsequent methods in the header as a Bearer Auth token.
        /// </summary>
        /// <param name="model">Contains the API Key and Secret</param>
        /// <returns>The authentication results</returns>
        [HttpPost("api/client/v1/tokenauth", Name = nameof(Authenticate))]
        [ProducesResponseType(typeof(AuthenticateResultModel), 200)]
        [DontWrapResult(WrapOnError = false, WrapOnSuccess = false, LogError = true)]
        public async Task<IActionResult> Authenticate([FromBody] ClientAuthenticateModel model)
        {
            /*
             * This 1st Authenticate() looks only in ApiKeys, which are assumed to be unique across Tenants (unlike Users),
             * thus we can pull back a TenantId on success and set the session to use it
             */
            var apiKeyAuthenticationResult = await _apiKeyAuthenticationService.Authenticate(model.ApiKey, model.Secret);

            if (!apiKeyAuthenticationResult.Success)
            {
                // this 401 is much cleaner than what the regular TokenAuthController returns.  It does a HttpFriendlyException which results in 500 :|
                return new UnauthorizedObjectResult(null);
            }

            using (_session.Use(apiKeyAuthenticationResult.TenantId, null))
            {
                /*
                 * This 2nd Authenticate is almost entirely guaranteed to succeed except for a few edge cases like if the
                 * tenant is inactive.  However, it's necessary in order to get a loginResult and create an access token.
                 */
                AbpLoginResult<Tenant, User> loginResult = await GetLoginResultAsync(
                    model.ApiKey,
                    model.Secret,
                    GetTenancyNameOrNull()
                );

                return new OkObjectResult(CreateAccessToken(loginResult));
            }
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            if (loginResult.Result != AbpLoginResultType.Success)
            {
                throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result,
                    usernameOrEmailAddress, tenancyName);
            }

            return loginResult;
        }

        private string GetTenancyNameOrNull()
        {
            if (!_session.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(_session.TenantId.Value)?.TenancyName;
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private AuthenticateResultModel CreateAccessToken(AbpLoginResult<Tenant, User> loginResult)
        {
            var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));

            return new AuthenticateResultModel
            {
                AccessToken = accessToken,
                EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                UserId = loginResult.User.Id
            };
        }

        private List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        private string GetEncrpyedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, AppConsts.DefaultPassPhrase);
        }
    }
}