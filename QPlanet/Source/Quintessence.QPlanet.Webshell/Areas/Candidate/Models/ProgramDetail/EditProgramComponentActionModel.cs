using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Cam;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate.Models.ProgramDetail
{
    public class EditProgramComponentActionModel
    {
        public List<DayPlanAssessorView> Assessors { get; set; }

        public EditProgramComponentModel ProgramComponent { get; set; }

        public IEnumerable<SelectListItem> CreateAssessorDropDownList(Guid? selectedAssessorId)
        {
            yield return new SelectListItem
                {
                    Selected = !selectedAssessorId.HasValue,
                    Value = null,
                    Text = string.Empty
                };

            foreach (var assessor in Assessors)
            {
                yield return new SelectListItem
                {
                    Selected = selectedAssessorId.GetValueOrDefault() == assessor.AssessorId,
                    Value = assessor.AssessorId.ToString(),
                    Text = assessor.FullName
                };
            }
        }
    }
}