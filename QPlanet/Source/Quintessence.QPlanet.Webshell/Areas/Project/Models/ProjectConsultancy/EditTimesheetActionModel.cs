using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.ObjectBuilder2;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QPlanet.ViewModel.Scm;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectConsultancy
{
    public class EditTimesheetActionModel
    {
        private List<InvoiceStatusType> _invoiceStatusses;
        public List<EditTimesheetEntryModel> Entries { get; set; }

        public List<CreateNewTimesheetEntryModel> UnregisteredEntries { get; set; }

        public List<ActivityProfileSelectListItemModelNullable> CreateUnregisteredActivityProfileSelectListItems(DateTime activityDate)
        {
            var list = new List<ActivityProfileSelectListItemModelNullable> { new ActivityProfileSelectListItemModelNullable(null) };

            var validProjectPlanPhaseActivities = ProjectPlan
                .ProjectPlanPhases
                .Where(ppp => ppp.StartDate <= activityDate && ppp.EndDate >= activityDate)
                .SelectMany(ppp => ppp.ProjectPlanPhaseEntries.OfType<ProjectPlanPhaseActivityView>());

            var validActivityProfiles = ActivityProfiles
                .Where(ap => validProjectPlanPhaseActivities.Any(pppa => pppa.ActivityId == ap.ActivityId && pppa.ProfileId == ap.ProfileId));

            list.AddRange(validActivityProfiles.Select(ap => new ActivityProfileSelectListItemModelNullable(ap)));
            return list;
        }

        public List<ActivityProfileSelectListItemModel> CreateRegisteredActivityProfileSelectListItems(DateTime activityDate, Guid selectedId)
        {
            var validProjectPlanPhaseActivities = ProjectPlan
                .ProjectPlanPhases
                .Where(ppp => ppp.StartDate <= activityDate && ppp.EndDate >= activityDate)
                .SelectMany(ppp => ppp.ProjectPlanPhaseEntries.OfType<ProjectPlanPhaseActivityView>());

            var validActivityProfiles = ActivityProfiles
                .Where(ap => validProjectPlanPhaseActivities.Any(pppa => pppa.ActivityId == ap.ActivityId && pppa.ProfileId == ap.ProfileId) || ap.Id == selectedId);

            var list = new List<ActivityProfileSelectListItemModel>(validActivityProfiles.Select(ap => new ActivityProfileSelectListItemModel(ap)));
            list.Where(i => i.Id == selectedId).ForEach(i => i.Selected = true);
            return list;
        }

        public IEnumerable<SelectListItem> CreateProjectPlanPhaseSelectListItems(DateTime date)
        {
            var list = new List<ProjectPlanPhaseSelectListItemModelNullable> { new ProjectPlanPhaseSelectListItemModelNullable(null) };
            list.AddRange(ProjectPlan.ProjectPlanPhases.OrderBy(ppp => ppp.StartDate).Where(ppp => ppp.StartDate <= date && ppp.EndDate >= date).Select(ppp => new ProjectPlanPhaseSelectListItemModelNullable(ppp)));
            return list;
        }

        public List<ActivityProfileView> ActivityProfiles { get; set; }

        public Guid ProjectId { get; set; }

        public bool IsProjectManager { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public Guid? UserId { get; set; }

        public ProjectPlanView ProjectPlan { get; set; }

        public List<ProjectPriceIndexView> ProjectPriceIndices { get; set; }

        public ConsultancyProjectView Project { get; set; }

        public IEnumerable<SelectListItem> CreateInvoiceStatusSelectListItem(int statusCode)
        {
            if (_invoiceStatusses == null)
                _invoiceStatusses = Enum.GetValues(typeof(InvoiceStatusType)).OfType<InvoiceStatusType>().ToList();

            if (IsProjectManager && (InvoiceStatusType)statusCode != InvoiceStatusType.Invoiced)
            {
                var values = Enum.GetValues(typeof(InvoiceStatusType)).Cast<InvoiceStatusType>();

                foreach (InvoiceStatusType item in values)
                {
                    if (item != InvoiceStatusType.Invoiced)
                    {
                        var result = CreateSelectListItem(item, ((int)item) == statusCode);
                        yield return result;
                    }
                }
            }
            else { 

            yield return CreateSelectListItem((InvoiceStatusType)statusCode, true);

            switch ((InvoiceStatusType)statusCode)
            {
                case InvoiceStatusType.Draft:
                    switch (Project.PricingModelType)
                    {
                        case PricingModelType.FixedPrice:
                            yield return CreateSelectListItem(InvoiceStatusType.FixedPrice);
                            break;
                        case PricingModelType.TimeAndMaterial:
                            yield return CreateSelectListItem(InvoiceStatusType.ToVerify);
                            break;
                    }
                    break;

                case InvoiceStatusType.ToVerify:
                    yield return CreateSelectListItem(InvoiceStatusType.Draft);
                    break;

                case InvoiceStatusType.ContestedByProma:
                    yield return CreateSelectListItem(InvoiceStatusType.Draft);
                    switch (Project.PricingModelType)
                    {
                        case PricingModelType.FixedPrice:
                            yield return CreateSelectListItem(InvoiceStatusType.FixedPrice);
                            break;
                        case PricingModelType.TimeAndMaterial:
                            yield return CreateSelectListItem(InvoiceStatusType.ToVerify);
                            break;
                    }
                    break;

                case InvoiceStatusType.FixedPrice:
                    yield return CreateSelectListItem(InvoiceStatusType.Draft);
                    break;
            }
            }
        }

        private SelectListItem CreateSelectListItem(InvoiceStatusType status, bool isSelected = false)
        {
            return new SelectListItem { Selected = isSelected, Text = EnumMemberNameAttribute.GetName(status), Value = ((int)status).ToString(CultureInfo.InvariantCulture) };
        }
    }
}