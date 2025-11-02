using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OAuthService.Options;

namespace OAuthService
{
    public static class DependencyInjection
    {
        public static void AddGoogleOAuthService(this IServiceCollection services,
           IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddHttpClient<ApiClientHelper>();
            services.Configure<GoogleOAuthOptions>(
     configuration.GetSection("OAuth:Google"));
            // services.AddScoped<IOAuthService, GoogleOAuthService>();
            services.AddKeyedScoped<IOAuthService, GoogleOAuthService>(OAuthProviderEnum.Google);
        }
    }
}
