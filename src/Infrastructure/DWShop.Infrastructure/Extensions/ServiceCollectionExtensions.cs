using DWShop.Application.Interfaces.Repositories;
using DWShop.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DWShop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IRepositoryAsync<,>),
                    typeof(RepositoryAsync<,>));
        }
    }
}
