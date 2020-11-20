using System;
using System.Threading.Tasks;
using Kenze.Infrastructure.Interfaces;
using Quintessence.QCandidate.Core.Domain;

namespace Quintessence.QCandidate.Infrastructure.EntityFrameworkCore.Commands
{
    public interface IMemoProgramComponentRepository : IRepository
    {
        Task<MemoProgramComponent> FindAsync(Guid id);

        MemoProgramComponent Add(MemoProgramComponent memoProgramComponent);
    }
}