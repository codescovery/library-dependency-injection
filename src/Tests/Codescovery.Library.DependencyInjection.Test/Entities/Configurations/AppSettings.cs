using System.Text.Json.Serialization;
using Codescovery.Library.Commons.Entities.Configurations;
using Codescovery.Library.DependencyInjection.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Codescovery.Library.DependencyInjection.Test.Entities.Configurations;

public class AppSettings:BaseAppSettings
{
    [JsonPropertyName("timeSpanConfiguration")]
    public TimeSpanConfiguration? TimeSpanConfiguration { get; set; }
}