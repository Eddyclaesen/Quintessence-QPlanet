using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quintessence.QCandidate.Contracts.Responses;

namespace Quintessence.QCandidate.Models
{
    public class AssessmentModel
    {
        public AssessmentModel(IMediator mediator, IUrlHelper urlHelper, AssessmentDto assessment)
        {
            Position = assessment?.Position?.Name;
            Customer = assessment?.Customer?.Name;
            Date = assessment?.DayProgram?.Date ?? DateTime.Now;
            Location = assessment?.DayProgram?.Location?.Name;
            Events = MapEvents(mediator, urlHelper, assessment);
        }

        private List<EventModel> MapEvents(IMediator mediator, IUrlHelper urlHelper, AssessmentDto assessment)
        {
            var result = new List<EventModel>();

            if(assessment?.DayProgram?.ProgramComponents == null)
            {
                return result;
            }

            result.AddRange(assessment.DayProgram.ProgramComponents.Select(pc => new EventModel(mediator, urlHelper, pc)));

            return result;
        }

        public string Customer { get; }
        public string Position { get; }
        public string Location { get; }
        public DateTime Date { get; }
        public List<EventModel> Events { get; }
    }
}