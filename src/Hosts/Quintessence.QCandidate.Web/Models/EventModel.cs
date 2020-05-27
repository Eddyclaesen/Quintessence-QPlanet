using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quintessence.QCandidate.Contracts.Responses;
using Quintessence.QCandidate.Core.Queries;
using Quintessence.QCandidate.Helpers;

namespace Quintessence.QCandidate.Models
{
    public class EventModel
    {
        public EventModel(IMediator mediator, IUrlHelper urlHelper, ProgramComponentDto programComponent)
        {
            if(programComponent == null)
            {
                return;
            }

            Title = programComponent.Description ?? programComponent.Name;
            Location = programComponent.Room.Name;
            DocumentName = programComponent.SimulationCombinationId.HasValue
                ? "PDF"
                : null;
            DocumentLink = programComponent.SimulationCombinationId.HasValue
                //&& c.Start.Date == DateTime.Today
                ? mediator.Send(new HasSimulationCombinationPdfByIdAndLanguageQuery(programComponent.SimulationCombinationId.Value, "NL")).Result
                    ? urlHelper.Action("Details", "ProgramComponents", new { id = programComponent.Id })
                    : null
                : null;
            Assessors = TimeslotHelper.GetAssessorsString(programComponent.LeadAssessor, programComponent.CoAssessor);
            Time = TimeslotHelper.GetTimeString(programComponent.Start, programComponent.End);
            Start = TimeslotHelper.CalculatePixelOffset(programComponent.Start);
            End = TimeslotHelper.CalculatePixelOffset(programComponent.End);
        }

        public string Title { get; }
        public string Location { get; }
        public string DocumentName { get; }
        public string DocumentLink { get; }
        public string Assessors { get; }
        public string Time { get; }
        public int Start { get; }
        public int End { get; }
    }
}