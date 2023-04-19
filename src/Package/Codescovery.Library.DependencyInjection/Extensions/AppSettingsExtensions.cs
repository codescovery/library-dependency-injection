using System;
using System.Collections.Generic;
using System.Linq;
using Codescovery.Library.DependencyInjection.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Codescovery.Library.DependencyInjection.Extensions
{
    public static class AppSettingsExtensions
    {
        public static TAppSettings AddAppSettings<TAppSettings>(this IServiceCollection services,
            IConfiguration configuration, string sectionName = BaseAppSettings.DefaultSectionName,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where TAppSettings : BaseAppSettings
        {
            var appSettingsSection = configuration.GetSection(sectionName);
            var appSettings = appSettingsSection.Get<TAppSettings>();
            if (appSettings == null) throw new NullReferenceException(nameof(appSettings));
            services.Configure<TAppSettings>(appSettingsSection);
            services.Add(new ServiceDescriptor(typeof(TAppSettings), serviceProvider => appSettings, lifetime));
            return appSettings;
        }
        public static TAppSettings AddAppSettings<TAppSettings>(this IServiceCollection services, IConfiguration configuration,
            TAppSettings appSettings,
            Func<IEnumerable<IOptionsChangeTokenSource<TAppSettings>>> optionsChangeTokenSourceBuilder = null,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where TAppSettings : BaseAppSettings
        {

            if (appSettings == null) throw new NullReferenceException(nameof(appSettings));
            var appSettingsConfigureOptions = configuration.CreateAppSettingsConfigureOptions<TAppSettings>();
            appSettingsConfigureOptions.Configure(appSettings);
            var appSettingsOptions = Options.Create(appSettings);
            var appSettingsMonitor = CreateAppSettingsMonitor(services, configuration, appSettingsConfigureOptions);
            services.Add(appSettingsOptions,lifetime);
            services.Add(appSettingsMonitor, lifetime);
            services.Add(new ServiceDescriptor(typeof(TAppSettings), serviceProvider => appSettings, lifetime));
            return appSettings;
        }

        private static IOptionsMonitor<TAppSettings> CreateAppSettingsMonitor<TAppSettings>(IServiceCollection services,
            IConfiguration configuration, ConfigureFromConfigurationOptions<TAppSettings> appSettingsConfigureOptions)
            where TAppSettings : BaseAppSettings
        {
            var appSettingsOptionsFactory = services.CreateAppSettingsOptionsFactory(appSettingsConfigureOptions);
            var appSettingsMonitor = new OptionsMonitor<TAppSettings>(appSettingsOptionsFactory,
                configuration.CreateAppSettingsOptionsChangeTokenSources<TAppSettings>(), new OptionsCache<TAppSettings>());
            return appSettingsMonitor;
        }

        private static IEnumerable<IOptionsChangeTokenSource<TAppSettings>> CreateAppSettingsOptionsChangeTokenSources<TAppSettings>(this IConfiguration configuration,
            Func<IEnumerable<IOptionsChangeTokenSource<TAppSettings>>> builder=null) where TAppSettings : BaseAppSettings
        {
            return builder?.Invoke()??Enumerable.Empty<IOptionsChangeTokenSource<TAppSettings>>();
        }

        public static ConfigureFromConfigurationOptions<TAppSettings> CreateAppSettingsConfigureOptions<TAppSettings>(this IConfiguration configuration) where TAppSettings : BaseAppSettings
        {
            return new ConfigureFromConfigurationOptions<TAppSettings>(configuration);
        }

        public static OptionsFactory<TAppSettings> CreateAppSettingsOptionsFactory<TAppSettings>(this IServiceCollection services,ConfigureFromConfigurationOptions<TAppSettings> appSettingsConfigureOptions) where TAppSettings : BaseAppSettings
        {
            return new OptionsFactory<TAppSettings>(new[] {appSettingsConfigureOptions},
                Enumerable.Empty<IPostConfigureOptions<TAppSettings>>());
        }
    }
}