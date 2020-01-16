using System;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.Business.Integration.Tests.Base
{
    [TestClass]
    public abstract class QueryRepositoryTestBase<TQueryRepository> : RepositoryTestBase
        where TQueryRepository : IQueryRepository 
    {
        [TestMethod]
        public void TestImplementations()
        {
            var repositoryMethods = typeof(TQueryRepository).GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var testMethods = GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);

            var missingMethods = repositoryMethods.Where(rm => !testMethods.Any(tm => tm.Name.Contains(rm.Name))).ToList();

            if (missingMethods.Count > 0)
                Assert.Fail(string.Join(Environment.NewLine, missingMethods.Select(mm => string.Format("No matching test-method found for {0}.{1}", typeof(TQueryRepository).Name, mm.Name))));
        }

        [TestMethod]
        public void TestList()
        {
            var repository = Container.Resolve<TQueryRepository>();

            Assert.IsNotNull(repository);

            var baseType = repository.GetType().BaseType;
            Assert.IsNotNull(baseType);

            var generics = baseType.GetGenericArguments().Where(t => t.GetInterface("IQuintessenceQueryContext") != null);

            var properties = generics.SelectMany(g => g.GetProperties());

            var listMethod = repository.GetType().GetMethod("List");
            foreach (var property in properties.Where(p => p.PropertyType.Name.StartsWith("IDbSet")))
            {
                var listMethodGeneric = listMethod.MakeGenericMethod(property.PropertyType.GetGenericArguments()[0]);
                var items = listMethodGeneric.Invoke(repository, new object[] { null });
                Assert.IsNotNull(items);
            }
        }

        [TestMethod]
        public void TestRetrieve()
        {
            var repository = Container.Resolve<TQueryRepository>();

            Assert.IsNotNull(repository);

            var baseType = repository.GetType().BaseType;
            Assert.IsNotNull(baseType);

            var generics = baseType.GetGenericArguments().Where(t => t.GetInterface("IQuintessenceQueryContext") != null);

            var properties = generics.SelectMany(g => g.GetProperties());

            var listMethod = repository.GetType().GetMethod("List");
            var retrieveMethod = repository.GetType().GetMethod("Retrieve");
            foreach (var property in properties.Where(p => p.PropertyType.Name.StartsWith("IDbSet")))
            {
                var listMethodGeneric = listMethod.MakeGenericMethod(property.PropertyType.GetGenericArguments()[0]);
                var items = listMethodGeneric.Invoke(repository, new object[] { null });
                Assert.IsNotNull(items);

                if (((dynamic)items).Count > 0)
                {
                    dynamic item = Enumerable.ElementAt((dynamic)items, 0);
                    Assert.IsNotNull(item);

                    var propertyGenericeArgument = property.PropertyType.GetGenericArguments()[0];

                    if (propertyGenericeArgument.BaseType != null && propertyGenericeArgument.BaseType == typeof(ViewEntityBase))
                    {
                        var itemId = item.Id;

                        var retrieveMethodGeneric = retrieveMethod.MakeGenericMethod(propertyGenericeArgument);
                        item = retrieveMethodGeneric.Invoke(repository, new object[] { item.Id });
                        Assert.IsNotNull(item);

                        Assert.AreEqual(itemId, item.Id);
                    }
                }
            }
        }
    }
}
