using MediatR;
using Quintessence.QCandidate.Core.Commands;
using Quintessence.QCandidate.Core.Domain;
using Quintessence.QCandidate.Infrastructure.EntityFrameworkCore.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Quintessence.QCandidate.Logic.Commands
{
    public class UpdateCalendarDayCommandHandler : IRequestHandler<UpdateCalendarDayCommand, MemoProgramComponent>
    {
        private readonly IMemoProgramComponentRepository _repository;

        public UpdateCalendarDayCommandHandler(IMemoProgramComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<MemoProgramComponent> Handle(UpdateCalendarDayCommand request, CancellationToken cancellationToken)
        {
            var memoProgram = await _repository.FindAsync(request.MemoProgramComponentId);

            foreach (var calendarDay in memoProgram.CalendarDays)
            {
                calendarDay.UpdateNote(request.Text);
            }

            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return memoProgram;
        }
    }
}
