using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Codescovery.Library.DependencyInjection.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Codescovery.Library.DependencyInjection.Extensions
{

    public static class DependencyInjectionExtensions
    {
        public static void Add<TService>(
            this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TService : class
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton<TService>();
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<TService>();
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<TService>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(serviceLifetime), (object) serviceLifetime, null);
            }
        }

        public static IEnumerable<Type> GetAllAutoImportTypes(this IServiceCollection services,
            Assembly assembly = null)
        {
            var persistedAssembly = assembly ?? Assembly.GetEntryAssembly();
            var types =  persistedAssembly?.GetTypes().Where(t => t.GetCustomAttribute<AutoImportAttribute>() != null)
                ?.ToList() ?? new List<Type>();
            return types;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, Assembly assembly = null)
        {
            var existingServicesTypes = services.GetAllAutoImportTypes(assembly);
            foreach (var serviceType in existingServicesTypes)
                AutoImportType(services, serviceType);

            return services;
        }

        private static void AutoImportType(this IServiceCollection services, Type serviceType)
        {
            var autoImportAttribute = serviceType.GetCustomAttribute<AutoImportAttribute>();
            if (autoImportAttribute == null) return;
            var allInterfaces = serviceType.GetInterfaces();
            var minimalInterfaces = (from iType in allInterfaces
                where !allInterfaces.Any(t => t.GetInterfaces()
                    .Contains(iType))
                select iType).ToList();
            foreach (var interfaceType in minimalInterfaces)
                services.Add(new ServiceDescriptor(interfaceType, serviceType, autoImportAttribute.ServiceLifetime));
            services.Add(new ServiceDescriptor(serviceType, serviceType, autoImportAttribute.ServiceLifetime));
        }

        public static void Add<TService, TImplementation>(
            this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TService : class
            where TImplementation : class, TService
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton<TService, TImplementation>();
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<TService, TImplementation>();
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<TService, TImplementation>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(serviceLifetime), (object) serviceLifetime, null);
            }
        }

        public static void Add<TService>(
            this IServiceCollection services,
            TService service,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where TService : class
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(service);
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped(_ => service);
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient(_ => service);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(serviceLifetime), (object) serviceLifetime, null);
            }
        }
    }
}