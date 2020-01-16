using System;
using System.Collections.Generic;
using System.Linq;
using Quintessence.QService.Core.Repository;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.Business.Interfaces.CommandRepositories
{
    public interface ICommandRepository : IRepository, IDisposable
    {
        void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void Delete<TEntity>(Guid id) where TEntity : class, IEntity;
        void DeleteList<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity;
        void DeleteList<TEntity>(IEnumerable<Guid> ids) where TEntity : class, IEntity;
        TEntity Retrieve<TEntity>(Guid id) where TEntity : class, IEntity;
        List<TEntity> List<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> filter = null) where TEntity : class;
        void Save<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void SaveList<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity;
        TEntity Prepare<TEntity>(Action<TEntity> extraAction = null) where TEntity : EntityBase, new();
    }
}
