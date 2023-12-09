using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using System.Configuration;

namespace Web_API.Providers
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        public IConfiguration Configuration { get; }

        public AuthenticationProvider(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));
                    //.AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));
        }
    }
}
