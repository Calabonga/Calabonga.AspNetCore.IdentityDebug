using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Calabonga.AspNetCore.IdentityDebug
{
    /// <summary>
    /// Authentication Handler for DEBUG 
    /// </summary>
    public class DebugAuthenticationSchemeHandler : AuthenticationHandler<DebugAuthenticationSchemeOptions>
    {
        private readonly DebugAuthenticationOptions _debugOptions;

        public DebugAuthenticationSchemeHandler(IOptionsMonitor<DebugAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock,
            DebugAuthenticationOptions debugOptions)
            : base(options, logger, encoder, clock)
        {
            _debugOptions = debugOptions;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var data = await File.ReadAllTextAsync(_debugOptions.JsonPath);
            var dictionary = JsonSerializer.Deserialize<DebugIdentity>(data);
            if (dictionary is null)
            {
                return AuthenticateResult.Fail("Authentication failed");
            }

            var claims = new List<Claim>();
            foreach (var item in dictionary.Items)
            {
                claims.Add(new Claim(GetClaimType(item.Key), item.Value));
            }

            var identity = new ClaimsIdentity(claims, "DEBUG");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

        private string GetClaimType(string key)
        {
            return key switch
            {
                "givenname" => ClaimTypes.GivenName,
                "surname" => ClaimTypes.Surname,
                "mail" => ClaimTypes.Email,
                "nameidentifier" => ClaimTypes.NameIdentifier,
                "name" => ClaimTypes.Name,
                "role" => ClaimTypes.Role,
                _ => key
            };
        }
    }
}
