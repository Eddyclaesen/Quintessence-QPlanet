using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.Business.QueryRepositories
{
    public class QueryRepositoryBase<TContext> : Base.RepositoryBase<TContext>, IQueryRepository
        where TContext : IQuintessenceQueryContext, IDisposable
    {
        public QueryRepositoryBase(IUnityContainer container)
            : base(container)
        {
        }

        ~QueryRepositoryBase()
        {
            Dispose(false);
        }

        public override TContext CreateContext()
        {
            var context = base.CreateContext();
            var dbContext = context as DbContext;

            if (dbContext != null)
            {
                dbContext.Configuration.AutoDetectChangesEnabled = false;
                dbContext.Configuration.LazyLoadingEnabled = false;
                dbContext.Configuration.ProxyCreationEnabled = false;
                dbContext.Configuration.ValidateOnSaveEnabled = false;
            }

            return context;
        }

        public TEntity Retrieve<TEntity>(Guid id) where TEntity : class, IViewEntity
        {
            using (DurationLog.Create())
            {
                try
                {
                    var propertyInfo = QueryRepositoryPropertyInfoLibrary.GetPropertyInfo<TEntity>(() => typeof(TContext).GetProperties().SingleOrDefault(pi => pi.PropertyType == typeof(DbQuery<TEntity>)));

                    if (propertyInfo == null)
                        throw new InvalidOperationException(string.Format("Property of type {0} not found on context {1}", typeof(DbQuery<TEntity>).Name, typeof(TContext).Name));

                    using (var context = CreateContext())
                    {
                        var set = ((DbQuery<TEntity>)propertyInfo.GetValue(context));
                        return set.SingleOrDefault(e => e.Id == id);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<TEntity> List<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> filter = null) where TEntity : class
        {
            using (DurationLog.Create())
            {
                try
                {
                    var propertyInfo = QueryRepositoryPropertyInfoLibrary.GetPropertyInfo<TEntity>(() => typeof(TContext).GetProperties().SingleOrDefault(pi => pi.PropertyType == typeof(DbQuery<TEntity>)));

                    if (propertyInfo == null)
                        throw new InvalidOperationException(string.Format("Property of type {0} not found on context {1}", typeof(DbQuery<TEntity>).Name, typeof(TContext).Name));

                    using (var context = CreateContext())
                    {
                        if (filter == null)
                            return ((DbQuery<TEntity>)propertyInfo.GetValue(context)).ToList();
                        return filter.Invoke(((DbQuery<TEntity>)propertyInfo.GetValue(context))).ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }
    }
}
