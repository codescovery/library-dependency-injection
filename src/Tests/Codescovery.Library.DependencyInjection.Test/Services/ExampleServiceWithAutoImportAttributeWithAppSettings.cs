using Codescovery.Library.DependencyInjection.Attributes;
using Codescovery.Library.DependencyInjection.Test.Entities.Configurations;
using Codescovery.Library.DependencyInjection.Test.Interfaces;
using Microsoft.Extensions.Options;

namespace Codescovery.Library.DependencyInjection.Test.Services
{
    [AutoImport]
    public class ExampleServiceWithAutoImportAttributeWithAppSettings 
    {
        public IOptions<AppSettings> AppSettingsOptions { get; }
        public AppSettings AppSettings { get; }
        public ExampleServiceWithAutoImportAttributeWithAppSettings(IOptions<AppSettings> appSettingsOptions, AppSettings appSettings)
        {
            AppSettingsOptions = appSettingsOptions;
            AppSettings = appSettings;
        }


    }
}
