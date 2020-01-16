using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectCandidateReportingOverview
{
    public class ProjectCandidateReportingOverviewRowModel
    {
        public ProjectCandidateReportingOverviewEntryView ReportingOverviewEntry { get; set; }

        public List<ReportStatusView> ReportStatuses { get; set; }

        public List<UserView> Users { get; set; }

        public IEnumerable<SelectListItem> CreateReportStatusDropDownList(int reportStatusId)
        {
            foreach (var reportStatus in ReportStatuses.OrderBy(rs => rs.SortOrder))
            {
                yield return new SelectListItem
                    {
                        Selected = reportStatus.Id == reportStatusId,
                        Value = reportStatus.Id.ToString(CultureInfo.InvariantCulture),
                        Text = reportStatus.Name
                    };
            }
        }

        public IEnumerable<SelectListItem> CreateUserDropDownList(Guid? userId)
        {
            yield return new SelectListItem
            {
                Selected = !userId.HasValue,
                Value = null,
                Text = string.Empty
            };

            foreach (var user in Users.OrderBy(u => u.LastName))
            {
                yield return new SelectListItem
                {
                    Selected = user.Id == userId,
                    Value = user.Id.ToString(),
                    Text = user.LastName + (string.IsNullOrEmpty(user.LastName) ? "" : ", ") + user.FirstName
                };
            }
        }
    }
}