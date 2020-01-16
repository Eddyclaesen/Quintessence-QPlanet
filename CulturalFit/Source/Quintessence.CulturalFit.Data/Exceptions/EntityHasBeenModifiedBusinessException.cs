using System;
using Quintessence.CulturalFit.Infra.Exceptions;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.Data.Exceptions
{
    public class EntityHasBeenModifiedBusinessException<TEntity> : BusinessException
        where TEntity : IEntity
    {

        public EntityHasBeenModifiedBusinessException(TEntity entity,  string message = null)
            : base(message ?? "Entity has already been modified by " + entity.Audit.ModifiedBy + " on " + entity.Audit.ModifiedOn)
        {
            Entity = entity;
            ModifiedBy = entity.Audit.ModifiedBy;
            ModifiedOn = entity.Audit.ModifiedOn.GetValueOrDefault();
        }

        public EntityHasBeenModifiedBusinessException(TEntity entity, string message, Exception innerException)
            : base(message ?? "Entity has already been modified by " + entity.Audit.ModifiedBy + " on " + entity.Audit.ModifiedOn, innerException)
        {
            Entity = entity;
            ModifiedBy = entity.Audit.ModifiedBy;
            ModifiedOn = entity.Audit.ModifiedOn.GetValueOrDefault();
        }

        public TEntity Entity { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
