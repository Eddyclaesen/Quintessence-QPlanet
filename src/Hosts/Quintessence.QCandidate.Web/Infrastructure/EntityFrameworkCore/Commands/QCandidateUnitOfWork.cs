using Kenze.Infrastructure.EntityFrameworkCore.Domain;
using Kenze.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Quintessence.QCandidate.Infrastructure.EntityFrameworkCore.Commands
{
    public class QCandidateUnitOfWork : UnitOfWork
    {
        public QCandidateUnitOfWork(DbContextOptions<DbContext> options, IPrincipalProvider principalProvider, IMediator mediator)
            : base(options, principalProvider, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("QCandidate");

            modelBuilder.ApplyConfiguration(new CalendarDayConfiguration());
            modelBuilder.ApplyConfiguration(new MemoConfiguration());
            modelBuilder.ApplyConfiguration(new MemoProgramComponentConfiguration());
        }
    }
}