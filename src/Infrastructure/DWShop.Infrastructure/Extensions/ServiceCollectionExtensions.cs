using DWShop.Application.Interfaces.Repositories;
using DWShop.Application.Interfaces.Services;
using DWShop.Infrastructure.Repositories;
using DWShop.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DWShop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IRepositoryAsync<,>),
                    typeof(RepositoryAsync<,>))
                .AddScoped<IAccountService, AccountService>();
        }
    }
}
