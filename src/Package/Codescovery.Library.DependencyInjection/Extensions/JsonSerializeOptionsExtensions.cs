using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace Codescovery.Library.DependencyInjection.Extensions
{
    public static class JsonSerializeOptionsExtensions
    {
        public static IServiceCollection AddJsonSerializeOptions(this IServiceCollection services,
            Func<JsonSerializerOptions> jsonSerializerOptionsBuilder,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            var options = jsonSerializerOptionsBuilder.Invoke();
            services.Add(options, lifetime);

            return services;
        }

        public static IServiceCollection AddJsonSerializeOptions(this IServiceCollection services,
            JsonSerializerOptions options,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            services.Add(options, lifetime);

            return services;
        }

        public static IServiceCollection AddDefaultJsonSerializeOptions(this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            services.AddJsonSerializeOptions(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            }, lifetime);

            return services;
        }
    }
}