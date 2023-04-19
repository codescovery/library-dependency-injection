using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codescovery.Library.Commons.Entities.Directory;
using Codescovery.Library.Commons.Extensions;
using Codescovery.Library.DependencyInjection.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Codescovery.Library.DependencyInjection.Extensions
{

    public static class ConfigurationBuilderExtensions
    {

        public static FolderPath GetCurrentDirectoryBasePath<T>(this T builder, params string[] paths)
            where T : IConfigurationBuilder
        {
            var pathsList = paths ?? new string[] { };
            var persistedPaths = pathsList.Prepend(Directory.GetCurrentDirectory());
            return Path.Combine(persistedPaths.ToArray());
        }

        public static IConfigurationBuilder InitializeAppConfiguration<T>(this T builder, FolderPath basePath = null,
            IReadOnlyList<JsonConfigurationSource> jsonConfigurationSources = null, bool addEnvironmentVariables = true, bool addCommandLineArgs = true, string[]? args=null)
            where T : IConfigurationBuilder

        {

            if (!basePath.IsNullOrDefault() && !string.IsNullOrEmpty(basePath) && !string.IsNullOrWhiteSpace(basePath))
                builder.SetBasePath(basePath);

            var persistedJsonConfigurationSources = jsonConfigurationSources ?? new List<JsonConfigurationSource>
                { new() {Optional = true, Path = AppSettingsDefaultValues.DefaultAppSettingsFileName, ReloadOnChange = true}};
            foreach (var jsonConfigurationSource in persistedJsonConfigurationSources)
                if (jsonConfigurationSource.Path != null)
                    builder.AddJsonFile(jsonConfigurationSource.Path, jsonConfigurationSource.Optional,
                        jsonConfigurationSource.ReloadOnChange);
            if(addEnvironmentVariables)
                builder.AddEnvironmentVariables();
            if (addCommandLineArgs && args != null)
                builder.AddCommandLine(args);
            return builder;
        }

    }
}