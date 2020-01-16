using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.DataModel.Crm;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.Business.Interfaces
{
    public interface ICrmRepository
    {
        /// <summary>
        /// Retrieves the associate.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        Associate RetrieveAssociate(string username);

        IList<TEntity> List<TEntity>(Func<IDbSet<TEntity>, IEnumerable<TEntity>> filter = null) where TEntity : class;

        TEntity GetById<TEntity>(Guid id, Func<IDbSet<TEntity>, IEnumerable<TEntity>> filter = null)
            where TEntity : class, IEntity;

        TEntity GetById<TEntity>(int id, Func<IDbSet<TEntity>, IEnumerable<TEntity>> filter = null)
            where TEntity : class, IIntEntity;

        IList<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> whereClause,
                                       Func<IDbSet<TEntity>, IEnumerable<TEntity>> filter = null)
            where TEntity : class;

        Project RetrieveProject(Guid id);
        Associate RetrieveAssociateByUserId(Guid userId);
    }
}
