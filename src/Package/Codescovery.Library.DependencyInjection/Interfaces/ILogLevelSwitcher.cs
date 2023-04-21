using Microsoft.Extensions.Logging;

namespace Codescovery.Library.DependencyInjection.Interfaces;

public interface ILogLevelSwitcher
{
    void SwitchGlobalLogLevel(LogLevel logLevel);
    void SwitchLogLevel<T>(LogLevel logLevel);
}