using System;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class ProjectPlanPhaseSelectListItemModelNullable : SelectListItem
    {
        public ProjectPlanPhaseSelectListItemModelNullable(ProjectPlanPhaseView projectPlanPhase)
        {
            if (projectPlanPhase != null)
            {
                Id = projectPlanPhase.Id;
                Name = string.Format("{0} ({1} - {2})", projectPlanPhase.Name, projectPlanPhase.StartDate.ToShortDateString(), projectPlanPhase.EndDate.ToShortDateString());
            }
        }

        public Guid? Id
        {
            get { return Value != null ? new Guid(Value) : (Guid?)null; }
            set { Value = value != null ? value.ToString() : null; }
        }

        public string Name
        {
            get { return Text; }
            set { Text = value; }
        }
    }
}