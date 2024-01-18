using System;
using Kenze.Domain;
using Quintessence.QCandidate.Core.Domain;

namespace Quintessence.QCandidate.Models.Assessments
{
    public class ProgramComponent
    {
        public ProgramComponent(Guid id, string title, string description, string location, bool showDetailsLink, string assessors, DateTime start, DateTime end, QCandidateLayout qCandidateLayout)
        {
            Id = id;
            Title = title;
            Description = description;
            Location = location;
            ShowDetailsLink = showDetailsLink;
            Assessors = assessors;
            Start = start;
            End = end;
            QCandidateLayout = qCandidateLayout;
        }

        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
        public string Location { get; }
        public bool ShowDetailsLink { get; }
        public string Assessors { get; }
        public DateTime Start { get; }
        public DateTime End { get; }
        public QCandidateLayout QCandidateLayout { get; }
    }
}