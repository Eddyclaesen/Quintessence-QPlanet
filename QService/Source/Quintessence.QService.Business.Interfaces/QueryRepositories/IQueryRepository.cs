using System;
using System.Collections.Generic;
using System.Linq;
using Quintessence.QService.Core.Repository;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.Business.Interfaces.QueryRepositories
{
    public interface IQueryRepository : IRepository, IDisposable
    {
        TEntity Retrieve<TEntity>(Guid id)
            where TEntity : class, IViewEntity;

        List<TEntity> List<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> filter = null)
            where TEntity : class;
    }
}
