using Codescovery.Library.DependencyInjection.Attributes;
using Codescovery.Library.DependencyInjection.Test.Interfaces;

namespace Codescovery.Library.DependencyInjection.Test.Services;
[AutoImport]
public class ExampleServiceWithAutoImportAttributeWithNestedDependency
{
    public ExampleServiceWithAutoImportAttributeWithNestedDependency(IInterface1 interface1, IInterface2 interface2)
    {
        Interface1 = interface1;
        Interface2 = interface2;
    }

    public IInterface1 Interface1 { get; }
    public IInterface2 Interface2 { get; }
}