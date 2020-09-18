﻿using System;
using MediatR;
using Quintessence.QCandidate.Contracts.Responses;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetAssessmentByCandidateIdAndDateQuery : IRequest<AssessmentDto>
    {
        public GetAssessmentByCandidateIdAndDateQuery(Guid candidateId, DateTime date, string language)
        {
            CandidateId = candidateId;
            Date = date;
            Language = language;
        }

        public Guid CandidateId { get; }
        public DateTime Date { get; }
        public string Language { get; set; }
    }
}