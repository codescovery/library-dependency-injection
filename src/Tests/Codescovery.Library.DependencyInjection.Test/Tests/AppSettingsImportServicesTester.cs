using System.Diagnostics;
using Codescovery.Library.Commons.Entities.Configurations;
using Codescovery.Library.DependencyInjection.Extensions;
using Codescovery.Library.DependencyInjection.Test.Entities.Configurations;
using Codescovery.Library.DependencyInjection.Test.Interfaces;
using Codescovery.Library.DependencyInjection.Test.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Codescovery.Library.DependencyInjection.Test.Tests
{
    [TestClass]
    public class AppSettingsImportServicesTester
    {
        [TestInitialize]
        public void Initialize()
        {
#if DEBUG
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
#endif
        }
        [TestMethod]
        public void InjectAppSettingsUsingFile()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.InitializeAppConfiguration(configurationBuilder.GetCurrentDirectoryBasePath("Configurations"));
            var services = new ServiceCollection();
            services.AddScoped<ExampleServiceWithAutoImportAttributeWithAppSettings>();
            services.AddAppSettings<AppSettings>(configurationBuilder.Build());
            var serviceProvider = services.BuildServiceProvider();
            var exampleServiceWithAutoImportAttributeWithAppSettings = serviceProvider.GetService<ExampleServiceWithAutoImportAttributeWithAppSettings>();
            Assert.IsNotNull(exampleServiceWithAutoImportAttributeWithAppSettings);
            Assert.IsNotNull(exampleServiceWithAutoImportAttributeWithAppSettings.AppSettingsOptions);
            Assert.IsNotNull(exampleServiceWithAutoImportAttributeWithAppSettings.AppSettings);
        }
        [TestMethod]
        public void InjectAppSettingsUsingCreatedEntity()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.InitializeAppConfiguration(configurationBuilder.GetCurrentDirectoryBasePath("Configurations"));
            var services = new ServiceCollection();
            services.AddScoped<ExampleServiceWithAutoImportAttributeWithAppSettings>();
            services.AddAppSettings<AppSettings>(configurationBuilder.Build(), new AppSettings
            {
                TimeSpanConfiguration = new TimeSpanConfiguration
                {
                    Minutes = 4
                }
            });
            var serviceProvider = services.BuildServiceProvider();
            var exampleServiceWithAutoImportAttributeWithAppSettings = serviceProvider.GetService<ExampleServiceWithAutoImportAttributeWithAppSettings>();
            Assert.IsNotNull(exampleServiceWithAutoImportAttributeWithAppSettings);
            Assert.IsNotNull(exampleServiceWithAutoImportAttributeWithAppSettings.AppSettingsOptions);
            Assert.IsNotNull(exampleServiceWithAutoImportAttributeWithAppSettings.AppSettings);
        }
    }
}