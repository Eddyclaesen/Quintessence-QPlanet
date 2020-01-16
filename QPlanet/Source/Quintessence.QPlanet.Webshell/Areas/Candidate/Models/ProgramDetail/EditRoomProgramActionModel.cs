using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate.Models.ProgramDetail
{
    public class EditRoomProgramActionModel
    {
        public List<EditProjectCandidateAssessmentRoomModel> ProjectCandidates { get; set; }

        public DateTime Date { get; set; }

        public Guid RoomId { get; set; }

        public IEnumerable<SelectListItem> CreateProjectCandidateList()
        {
            yield return new SelectListItem { Value = Guid.Empty.ToString() };
            foreach (var projectCandidate in ProjectCandidates)
            {
                yield return new SelectListItem
                {
                    Selected = projectCandidate.AssessmentRoomId.HasValue && projectCandidate.AssessmentRoomId.Value == RoomId,
                    Value = projectCandidate.Id.ToString(),
                    Text = projectCandidate.CandidateFullName
                };
            }
        }

        public Guid? GetProjectCandidateId()
        {
            var projectCandidate = ProjectCandidates.FirstOrDefault(pc => pc.AssessmentRoomId == RoomId);

            if (projectCandidate != null)
                return projectCandidate.Id;
            return null;
        }

        public EditProjectCandidateAssessmentRoomModel GetProjectCandidate()
        {
            return ProjectCandidates.FirstOrDefault(pc => pc.AssessmentRoomId == RoomId);
        }
    }
}