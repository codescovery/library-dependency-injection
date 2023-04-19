using System.Diagnostics;
using Codescovery.Library.DependencyInjection.Extensions;
using Codescovery.Library.DependencyInjection.Test.Interfaces;
using Codescovery.Library.DependencyInjection.Test.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Codescovery.Library.DependencyInjection.Test.Tests
{
    [TestClass]
    public class AutoImportServicesTester
    {
        [TestInitialize]
        public void Initialize()
        {
#if DEBUG
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
#endif
        }
        [TestMethod]
        public void AutoImportServices()
        {
            var services = new ServiceCollection();
            services.AddServices(GetType().Assembly);
            var serviceProvider = services.BuildServiceProvider();
            var exampleServiceWithAutoImportAttribute = serviceProvider.GetService<IInterface1>();
            var exampleServiceWithAutoImportAttribute2 = serviceProvider.GetService<IInterface2>();
            var exampleServiceWithAutoImportAttributeWithNestedDependency = serviceProvider.GetService<ExampleServiceWithAutoImportAttributeWithNestedDependency>();
            Assert.IsNotNull(exampleServiceWithAutoImportAttribute);
            Assert.IsNotNull(exampleServiceWithAutoImportAttribute2);
            Assert.IsNotNull(exampleServiceWithAutoImportAttributeWithNestedDependency);
            Assert.IsNotNull(exampleServiceWithAutoImportAttributeWithNestedDependency.Interface1);
            Assert.IsNotNull(exampleServiceWithAutoImportAttributeWithNestedDependency.Interface2);
        }
    }
}