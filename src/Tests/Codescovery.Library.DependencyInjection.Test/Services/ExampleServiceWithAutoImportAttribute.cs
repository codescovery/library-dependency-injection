using Codescovery.Library.DependencyInjection.Attributes;
using Codescovery.Library.DependencyInjection.Test.Interfaces;

namespace Codescovery.Library.DependencyInjection.Test.Services
{
    [AutoImport]
    public class ExampleServiceWithAutoImportAttribute:IInterface1,IInterface2
    {
        public void Method1()
        {
            throw new NotImplementedException();
        }

        public void Method2()
        {
            throw new NotImplementedException();
        }
    }
}
