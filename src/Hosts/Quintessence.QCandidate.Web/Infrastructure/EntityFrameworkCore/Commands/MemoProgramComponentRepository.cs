using System;
using System.Threading.Tasks;
using Kenze.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Quintessence.QCandidate.Core.Domain;

namespace Quintessence.QCandidate.Infrastructure.EntityFrameworkCore.Commands
{
    public class MemoProgramComponentRepository : IMemoProgramComponentRepository
    {
        private readonly QCandidateUnitOfWork _unitOfWork;

        public MemoProgramComponentRepository(QCandidateUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork => _unitOfWork;

        public MemoProgramComponent Add(MemoProgramComponent memoProgramComponent)
        {
            _unitOfWork.Set<MemoProgramComponent>().Add(memoProgramComponent);
            _unitOfWork.SaveChanges();

            return memoProgramComponent;
        }

        public Task<MemoProgramComponent> FindAsync(Guid id)
        {
            return _unitOfWork.Set<MemoProgramComponent>()
                .Include(mpc => mpc.Memos)
                .Include(mpc => mpc.CalendarDays)
                .SingleOrDefaultAsync(mpc => mpc.Id == id);
        }



    }
}