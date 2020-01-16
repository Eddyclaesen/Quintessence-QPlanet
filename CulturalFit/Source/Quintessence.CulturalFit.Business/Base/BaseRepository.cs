using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using Quintessence.CulturalFit.Data.Interfaces;
using Quintessence.CulturalFit.Infra.Exceptions;
using Quintessence.CulturalFit.Infra.Logging;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.Business.Base
{
    public abstract class BaseRepository
    {
        #region Private fields
        private readonly IUnityContainer _container;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        protected BaseRepository(IUnityContainer container)
        {
            _container = container;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Unity container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        protected IUnityContainer Container
        {
            get { return _container; }
        }
        #endregion

        #region Generic methods
        /// <summary>
        /// Gets the entity by id (integer).
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public TEntity GetById<TEntity>(int id, Func<IDbSet<TEntity>, IEnumerable<TEntity>> filter = null)
            where TEntity : class, IIntEntity
        {
            using (new DurationLog())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        //Get properties of context
                        var properties = context.GetType().GetProperties();

                        //Get property that is of type IDbSet<TEntity>, e.g. IDbSet<TheoremListRequest>
                        var property = properties.FirstOrDefault(p => p.PropertyType == typeof(IDbSet<TEntity>));

                        if (property != null)
                        {
                            //Get value of property from context and cast to IDbSet
                            var dbset = property.GetValue(context) as IDbSet<TEntity>;

                            if (dbset != null)
                            {
                                //if there is no Include expression, just return the set without the Include
                                if (filter != null)
                                    return filter.Invoke(dbset).Single(s => s.Id == id);
                                return dbset.Single(s => s.Id == id);
                            }
                        }
                        throw new NotImplementedException(string.Format("Context does not contain property of type IDbSet<{0}>", typeof(TEntity).Name));
                    }
                }
                catch (Exception exception)
                {
                    var message = string.Format("Unable to retrieve {0} with id {1}.", typeof(TEntity), id);
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }

        /// <summary>
        /// Gets the entity by id (Guid).
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public TEntity GetById<TEntity>(Guid id, Func<IDbSet<TEntity>, IEnumerable<TEntity>> filter = null)
            where TEntity : class, IEntity
        {
            using (new DurationLog())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        //Get properties of context
                        var properties = context.GetType().GetProperties();

                        //Get property that is of type IDbSet<TEntity>, e.g. IDbSet<TheoremListRequest>
                        var property = properties.FirstOrDefault(p => p.PropertyType == typeof(IDbSet<TEntity>));

                        if (property != null)
                        {
                            //Get value of property from context and cast to IDbSet
                            var dbset = property.GetValue(context) as IDbSet<TEntity>;

                            if (dbset != null)
                            {
                                //if there is no Include expression, just return the set without the Include
                                if (filter != null)
                                    return filter.Invoke(dbset).Single(s => s.Id == id);
                                return dbset.Single(s => s.Id == id);
                            }
                        }
                        throw new NotImplementedException(string.Format("Context does not contain property of type IDbSet<{0}>", typeof(TEntity).Name));
                    }
                }
                catch (Exception exception)
                {
                    var message = string.Format("Unable to retrieve {0} with id {1}.", typeof(TEntity), id);
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }

        /// <summary>
        /// Filters the specified where clause.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="whereClause">The where clause.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <exception cref="Quintessence.CulturalFit.Infra.Exceptions.BusinessException"></exception>
        public IList<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> whereClause, Func<IDbSet<TEntity>, IEnumerable<TEntity>> filter = null)
            where TEntity : class
        {
            using (new DurationLog())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        //Get properties of context
                        var properties = context.GetType().GetProperties();

                        //Get property that is of type IDbSet<TEntity>, e.g. IDbSet<TheoremListRequest>
                        var property = properties.FirstOrDefault(p => p.PropertyType == typeof(IDbSet<TEntity>));

                        if (property != null)
                        {
                            //Get value of property from context and cast to IDbSet
                            var dbset = property.GetValue(context) as IDbSet<TEntity>;

                            if (dbset != null)
                            {
                                //if there is no Include expression, just return the set without the Include
                                if (filter != null)
                                    return filter.Invoke(dbset).Where(whereClause.Compile()).ToList();
                                return dbset.Where(whereClause).ToList();
                            }
                        }
                        throw new NotImplementedException(string.Format("Context does not contain property of type IDbSet<{0}>", typeof(TEntity).Name));
                    }
                }
                catch (Exception exception)
                {
                    var message = string.Format("Unable to filter {0}.", typeof(TEntity));
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }

        /// <summary>
        /// Adds the specified entity to the context and saves it.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <exception cref="Quintessence.CulturalFit.Infra.Exceptions.BusinessException"></exception>
        public TEntity Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            using (new DurationLog())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        //Get properties of context
                        var properties = context.GetType().GetProperties();

                        //Get property that is of type IDbSet<TEntity>, e.g. IDbSet<TheoremListRequest>
                        var property = properties.FirstOrDefault(p => p.PropertyType == typeof(IDbSet<TEntity>));

                        if (property != null)
                        {
                            //Get value of property from context and cast to IDbSet
                            var dbset = property.GetValue(context) as IDbSet<TEntity>;

                            if (dbset != null)
                            {
                                dbset.Add(entity);
                                context.SaveChanges();
                                return entity;
                            }
                        }
                        throw new NotImplementedException(string.Format("Context does not contain property of type IDbSet<{0}>", typeof(TEntity).Name));
                    }
                }
                catch (Exception exception)
                {
                    var message = string.Format("Unable to add {0}.", typeof(TEntity));
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }

        /// <summary>
        /// Lists the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<TEntity> List<TEntity>(Func<IDbSet<TEntity>, IEnumerable<TEntity>> filter = null)
            where TEntity : class
        {
            using (new DurationLog())
            {
                using (var context = CreateContext())
                {
                    var properties = context.GetType().GetProperties();

                    var property = properties.FirstOrDefault(p => p.PropertyType == typeof(IDbSet<TEntity>));

                    if (property != null)
                    {
                        var set = property.GetValue(context) as IDbSet<TEntity>;

                        if (set != null)
                        {
                            if (filter != null)
                                return filter.Invoke(set).ToList();
                            return set.ToList();
                        }
                    }
                    throw new NotImplementedException(string.Format("Context does not contain property of type IDbSet<{0}>", typeof(TEntity).Name));
                }
            }
        }
        #endregion

        #region Other methods
        /// <summary>
        /// Creates the context.
        /// </summary>
        /// <returns></returns>
        public IQContext CreateContext()
        {
            var context = Container.Resolve<IQContext>();
            return context;
        }

        /// <summary>
        /// Creates the audit.
        /// </summary>
        /// <returns></returns>
        public Audit CreateAudit()
        {
            var domainUser = string.Format(@"{0}\{1}", Environment.UserDomainName, Environment.UserName);
            return new Audit { CreatedBy = domainUser, CreatedOn = DateTime.Now, IsDeleted = false, VersionId = Guid.NewGuid() };
        }
        #endregion
    }
}
