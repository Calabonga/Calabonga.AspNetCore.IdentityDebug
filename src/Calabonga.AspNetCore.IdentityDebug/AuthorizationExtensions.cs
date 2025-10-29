using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.AspNetCore.IdentityDebug
{
    public static class AuthorizationExtensions
    {
        /// <summary>
        /// Add authentication for DEBUG
        /// </summary>
        /// <param name="services"></param>
        /// <param name="jsonPath"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddAuthenticationDebug(this IServiceCollection services, string jsonPath)
        {
            var options = new DebugAuthenticationOptions(jsonPath);
            services.AddScoped(_ => options);

            return services.AddAuthentication("DEBUG").AddScheme<DebugAuthenticationSchemeOptions, DebugAuthenticationSchemeHandler>("DEBUG", _ => { });
        }
    }
}
