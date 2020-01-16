using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quintessence.QService.Business.Integration.Tests.Base
{
    [TestClass]
    public abstract class CommandServiceImplementationTestBase<TCommandService> : ServiceImplementationTestBase
    {
        [TestMethod]
        public void TestImplementations()
        {
            var repositoryMethods = typeof(TCommandService).GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var testMethods = GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);

            var missingMethods = repositoryMethods.Where(rm => !testMethods.Any(tm => tm.Name.Contains(rm.Name))).ToList();

            if (missingMethods.Count > 0)
                Assert.Fail(string.Join(Environment.NewLine, missingMethods.Select(mm => string.Format("No matching test-method found for {0}.{1}", typeof(TCommandService).Name, mm.Name))));
        }
    }
}