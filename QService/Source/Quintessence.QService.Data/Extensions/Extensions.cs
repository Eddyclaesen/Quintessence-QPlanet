using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Practices.Unity;
using Quintessence.QService.Core.Security;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.Data.Extensions
{
    public static class Extensions
    {
        public static TEntity Create<TEntity>(this IQuintessenceCommandContext context)
            where TEntity : EntityBase, new()
        {
            var securityContext = context.Container.Resolve<SecurityContext>();

            var entity = new TEntity
            {
                Id = Guid.NewGuid(),
                Audit = new Audit
                {
                    CreatedBy = securityContext.UserName,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    VersionId = Guid.NewGuid()
                }
            };

            return entity;
        }

        public static TEntity CreateOrUpdate<TContext, TEntity>(this TContext context, Expression<Func<TContext, IDbSet<TEntity>>> dbSetExpression, TEntity entity, params Type[] baseTypes)
            where TEntity : class, IEntity
            where TContext : IQuintessenceCommandContext
        {
            var dbSet = dbSetExpression.Compile().Invoke(context);
            var storeEntity = dbSet.SingleOrDefault(e => e.Id == entity.Id);

            if (storeEntity == null)
            {
                dbSet.Add(entity);
            }
            else
            {
                var securityContext = context.Container.Resolve<SecurityContext>();

                Mapper.DynamicMap(entity, storeEntity, typeof(TEntity), typeof(TEntity));

                foreach (var baseType in baseTypes)
                {
                    if (entity.GetType() == baseType)
                    {
                        var convertedEntity = Convert.ChangeType(entity, baseType);
                        var convertedStoreEntity = Convert.ChangeType(storeEntity, baseType);

                        Mapper.DynamicMap(convertedEntity, convertedStoreEntity, baseType, baseType);
                    }
                }

                storeEntity.Audit.ModifiedOn = DateTime.Now;
                storeEntity.Audit.ModifiedBy = securityContext.UserName;
                storeEntity.Audit.VersionId = Guid.NewGuid();
            }

            return entity;
        }

        public static void Undelete<TContext, TEntity>(this TContext context, TEntity entity)
            where TEntity : class, IEntity
            where TContext : IQuintessenceCommandContext
        {
            if (!entity.Audit.IsDeleted)
                return;

            entity.Audit.IsDeleted = false;
            entity.Audit.DeletedBy = null;
            entity.Audit.DeletedOn = null;
            entity.Audit.VersionId = Guid.NewGuid();
        }

        public static void Delete<TContext, TEntity>(this TContext context, TEntity entity)
            where TEntity : class, IEntity
            where TContext : IQuintessenceCommandContext
        {
            if (entity.Audit.IsDeleted)
                return;

            var securityContext = context.Container.Resolve<SecurityContext>();
            entity.Audit.IsDeleted = true;
            entity.Audit.DeletedBy = securityContext.UserName;
            entity.Audit.DeletedOn = DateTime.Now;
            entity.Audit.VersionId = Guid.NewGuid();
        }
    }
}
