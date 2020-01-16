using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectConsultancy
{
    public class EditProductsheetActionModel
    {
        private List<CreateNewProductsheetEntryModel> _projectPlanPhaseProducts;
        private List<InvoiceStatusType> _invoiceStatusses;

        public Guid ProjectId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public ProjectPlanView ProjectPlan { get; set; }

        public List<EditProductsheetEntryModel> Entries { get; set; }

        public List<CreateNewProductsheetEntryModel> ProjectPlanPhaseProducts
        {
            get { return _projectPlanPhaseProducts ?? (_projectPlanPhaseProducts = new List<CreateNewProductsheetEntryModel>()); }
        }

        public ConsultancyProjectView Project { get; set; }

        public List<ProjectPriceIndexView> ProjectPriceIndices { get; set; }

        public IEnumerable<SelectListItem> CreateInvoiceStatusSelectListItem(int statusCode)
        {
            if (_invoiceStatusses == null)
                _invoiceStatusses = Enum.GetValues(typeof(InvoiceStatusType)).OfType<InvoiceStatusType>().ToList();

            switch ((InvoiceStatusType)statusCode)
            {
                case InvoiceStatusType.Draft:
                    switch (Project.PricingModelType)
                    {
                        case PricingModelType.FixedPrice:
                            yield return CreateSelectListItem(InvoiceStatusType.Draft, true);
                            yield return CreateSelectListItem(InvoiceStatusType.FixedPrice);
                            break;
                        case PricingModelType.TimeAndMaterial:
                            yield return CreateSelectListItem(InvoiceStatusType.Draft, true);
                            yield return CreateSelectListItem(InvoiceStatusType.ReadyForApproval);
                            yield return CreateSelectListItem(InvoiceStatusType.ReadyForInvoicing);
                            break;
                    }
                    break;

                
                    yield return CreateSelectListItem(InvoiceStatusType.Draft);
                    yield return CreateSelectListItem(InvoiceStatusType.ReadyForApproval, true);
                    yield return CreateSelectListItem(InvoiceStatusType.ReadyForInvoicing);
                    break;

                case InvoiceStatusType.ReadyForInvoicing:
                    yield return CreateSelectListItem(InvoiceStatusType.ReadyForApproval);
                    yield return CreateSelectListItem(InvoiceStatusType.ReadyForInvoicing, true);
                    yield return CreateSelectListItem(InvoiceStatusType.Invoiced);
                    break;

                case InvoiceStatusType.FixedPrice:
                    yield return CreateSelectListItem(InvoiceStatusType.Draft);
                    yield return CreateSelectListItem(InvoiceStatusType.FixedPrice, true);
                    yield return CreateSelectListItem(InvoiceStatusType.Invoiced);
                    break;

                case InvoiceStatusType.Invoiced:
                    yield return CreateSelectListItem(InvoiceStatusType.Invoiced, true);
                    break;
            }
        }

        private SelectListItem CreateSelectListItem(InvoiceStatusType status, bool isSelected = false)
        {
            return new SelectListItem { Selected = isSelected, Text = EnumMemberNameAttribute.GetName(status), Value = ((int)status).ToString(CultureInfo.InvariantCulture) };
        }
    }
}