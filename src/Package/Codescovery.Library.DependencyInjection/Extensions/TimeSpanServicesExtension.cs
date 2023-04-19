using Codescovery.Library.Commons.Builders;
using Codescovery.Library.Commons.Interfaces.TimeSpan;
using Microsoft.Extensions.DependencyInjection;

namespace Codescovery.Library.DependencyInjection.Extensions;

public static class TimeSpanServicesExtension
{
    public static IServiceCollection AddTimeSpanServices(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        services.Add(TimeSpanBuilder.BuildDefaultTimeSpanService(), serviceLifetime);
        return services;
    }
    public static IServiceCollection AddTimeSpanServices<T>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    where T : class, ITimeSpanService
    {
        services.Add<T>(serviceLifetime);
        return services;
    }
}