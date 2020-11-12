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
            var memoProgram = _repository.FindAsync(request.MemoProgramComponentId).Result;

            foreach (var calendarDay in memoProgram.CalendarDays)
            {
                if (calendarDay.Id == request.CalendarDayId)
                {
                    calendarDay.UpdateNote(request.Note);
                }
            }

            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return memoProgram;
        }
    }
}
