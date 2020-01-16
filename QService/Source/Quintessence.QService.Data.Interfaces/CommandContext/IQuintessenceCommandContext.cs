using System;
using System.Data.Entity.Infrastructure;
using Microsoft.Practices.Unity;

namespace Quintessence.QService.Data.Interfaces.CommandContext
{
    /// <summary>
    /// Interface for the Quintessence data context
    /// </summary>
    public interface IQuintessenceCommandContext : IDisposable
    {
        IUnityContainer Container { get; }
        int SaveChanges();
        DbEntityEntry Entry(Object entity);
    }
}
