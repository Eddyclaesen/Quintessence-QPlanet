using System;
using System.Data.Entity;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.Infra.Data
{
    public static class Extensions
    {
        public static TEntity CreateEntity<TEntity>(this IDbSet<TEntity> set)
            where TEntity : class, IEntity
        {
            var entity = set.Create();

            entity.Id = Guid.NewGuid();

            entity.Audit = new Audit
                               {
                                   CreatedBy = Environment.UserName,
                                   CreatedOn = DateTime.Now,
                                   IsDeleted = false,
                                   VersionId = Guid.NewGuid()
                               };

            return entity;
        }
    }
}
