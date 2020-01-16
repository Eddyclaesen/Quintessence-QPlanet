using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.Infrastructure.Model.DataModel;
using Quintessence.QService.Data.Extensions;

namespace Quintessence.QService.Business.CommandRepositories
{
    public abstract class CommandRepositoryBase<TContext> : Base.RepositoryBase<TContext>, ICommandRepository
        where TContext : IQuintessenceCommandContext
    {
        protected CommandRepositoryBase(IUnityContainer container)
            : base(container)
        {
        }

        ~CommandRepositoryBase()
        {
            Dispose(false);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            Delete<TEntity>(entity.Id);
        }

        public void Delete<TEntity>(Guid id)
            where TEntity : class, IEntity
        {
            using (DurationLog.Create())
            {
                try
                {
                    var propertyInfo = CommandRepositoryPropertyInfoLibrary
                        .GetPropertyInfo<TEntity>(() => typeof(TContext).GetProperties().SingleOrDefault(pi => pi.PropertyType == typeof(IDbSet<TEntity>)));

                    if (propertyInfo == null)
                        throw new InvalidOperationException(
                            string.Format("Property of type {0} not found on context {1}", typeof(IDbSet<TEntity>).Name,
                                          typeof(TContext).Name));

                    using (var context = CreateContext())
                    {
                        var entity = ((IDbSet<TEntity>)propertyInfo.GetValue(context)).SingleOrDefault(e => e.Id == id);
                        context.Delete(entity);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteList<TEntity>(IEnumerable<TEntity> entities)
             where TEntity : class, IEntity
        {
            using (DurationLog.Create())
            {
                try
                {
                    var propertyInfo = CommandRepositoryPropertyInfoLibrary
                        .GetPropertyInfo<TEntity>(() => typeof(TContext).GetProperties().SingleOrDefault(pi => pi.PropertyType == typeof(IDbSet<TEntity>)));

                    if (propertyInfo == null)
                        throw new InvalidOperationException(
                            string.Format("Property of type {0} not found on context {1}", typeof(IDbSet<TEntity>).Name,
                                          typeof(TContext).Name));

                    using (var context = CreateContext())
                    {
                        foreach (var entity in entities)
                            context.Delete(entity);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteList<TEntity>(IEnumerable<Guid> ids) where TEntity : class, IEntity
        {
            using (DurationLog.Create())
            {
                try
                {
                    var propertyInfo = CommandRepositoryPropertyInfoLibrary
                        .GetPropertyInfo<TEntity>(() => typeof(TContext).GetProperties().SingleOrDefault(pi => pi.PropertyType == typeof(IDbSet<TEntity>)));

                    if (propertyInfo == null)
                        throw new InvalidOperationException(
                            string.Format("Property of type {0} not found on context {1}", typeof(IDbSet<TEntity>).Name,
                                          typeof(TContext).Name));

                    using (var context = CreateContext())
                    {
                        foreach (var entity in ((IDbSet<TEntity>)propertyInfo.GetValue(context)).Where(e => ids.Contains(e.Id)))
                            context.Delete(entity);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public TEntity Retrieve<TEntity>(Guid id)
            where TEntity : class, IEntity
        {
            using (DurationLog.Create())
            {
                try
                {
                    var propertyInfo = CommandRepositoryPropertyInfoLibrary
                        .GetPropertyInfo<TEntity>(() => typeof(TContext).GetProperties().SingleOrDefault(pi => pi.PropertyType == typeof(IDbSet<TEntity>)));

                    if (propertyInfo == null)
                        throw new InvalidOperationException(
                            string.Format("Property of type {0} not found on context {1}", typeof(IDbSet<TEntity>).Name,
                                          typeof(TContext).Name));

                    using (var context = CreateContext())
                    {
                        return ((IDbSet<TEntity>)propertyInfo.GetValue(context)).SingleOrDefault(e => e.Id == id);
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
                    var propertyInfo = CommandRepositoryPropertyInfoLibrary
                        .GetPropertyInfo<TEntity>(() => typeof(TContext).GetProperties().SingleOrDefault(pi => pi.PropertyType == typeof(IDbSet<TEntity>)));

                    if (propertyInfo == null)
                        throw new InvalidOperationException(string.Format("Property of type {0} not found on context {1}", typeof(IDbSet<TEntity>).Name, typeof(TContext).Name));

                    using (var context = CreateContext())
                    {
                        if (filter == null)
                            return ((IDbSet<TEntity>)propertyInfo.GetValue(context)).ToList();
                        return filter.Invoke(((IDbSet<TEntity>)propertyInfo.GetValue(context))).ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void Save<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            using (DurationLog.Create())
            {
                try
                {
                    var propertyInfo = CommandRepositoryPropertyInfoLibrary
                        .GetPropertyInfo<TEntity>(() => typeof(TContext).GetProperties().SingleOrDefault(pi => pi.PropertyType == typeof(IDbSet<TEntity>)));

                    if (propertyInfo == null)
                        throw new InvalidOperationException(
                            string.Format("Property of type {0} not found on context {1}", typeof(IDbSet<TEntity>).Name,
                                          typeof(TContext).Name));

                    if (Container.Resolve<ValidationRuleEngine>().Validate(Container, entity))
                    {
                        using (var context = CreateContext())
                        {
                            context.CreateOrUpdate(c => ((IDbSet<TEntity>)propertyInfo.GetValue(c)), entity);
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void SaveList<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity
        {
            using (DurationLog.Create())
            {
                try
                {
                    var propertyInfo = CommandRepositoryPropertyInfoLibrary
                        .GetPropertyInfo<TEntity>(() => typeof(TContext).GetProperties().SingleOrDefault(pi => pi.PropertyType == typeof(IDbSet<TEntity>)));

                    if (propertyInfo == null)
                        throw new InvalidOperationException(
                            string.Format("Property of type {0} not found on context {1}", typeof(IDbSet<TEntity>).Name,
                                          typeof(TContext).Name));

                        using (var context = CreateContext())
                        {
                            foreach (var entity in entities.Where(entity => entity != null && Container.Resolve<ValidationRuleEngine>().Validate(Container, entity)))
                                context.CreateOrUpdate(c => ((IDbSet<TEntity>)propertyInfo.GetValue(c)), entity);
                            context.SaveChanges();
                        }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public TEntity Prepare<TEntity>(Action<TEntity> extraAction = null) where TEntity : EntityBase, new()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var propertyInfo = CommandRepositoryPropertyInfoLibrary
                        .GetPropertyInfo<TEntity>(() => typeof(TContext).GetProperties().SingleOrDefault(pi => pi.PropertyType == typeof(IDbSet<TEntity>)));

                    if (propertyInfo == null)
                        throw new InvalidOperationException(
                            string.Format("Property of type {0} not found on context {1}", typeof (IDbSet<TEntity>).Name,
                                          typeof (TContext).Name));

                    using (var context = CreateContext())
                    {
                        return context.Create<TEntity>();
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
