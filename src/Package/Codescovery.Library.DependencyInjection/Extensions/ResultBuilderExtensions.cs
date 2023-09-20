using Codescovery.Library.Commons.Builders;
using Codescovery.Library.Commons.Interfaces.Result;
using Microsoft.Extensions.DependencyInjection;

namespace Codescovery.Library.DependencyInjection.Extensions
{
    public static class ResultBuilderExtensions
    {

        public static IServiceCollection AddResultBuilderService(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            services.Add(ResultBuilder.Default(),serviceLifetime);
            return services;
        }
        public static IServiceCollection AddResultBuilderService<T>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        where T : class, IResultBuilderService
        {
            services.Add(ResultBuilder.Default(), serviceLifetime);
            return services;
        }
    }
}
