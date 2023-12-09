using Microsoft.Extensions.DependencyInjection;

namespace Domain.Interfaces
{
    public interface IAuthenticationProvider
    {
        void ConfigureServices(IServiceCollection services);
    }
}
