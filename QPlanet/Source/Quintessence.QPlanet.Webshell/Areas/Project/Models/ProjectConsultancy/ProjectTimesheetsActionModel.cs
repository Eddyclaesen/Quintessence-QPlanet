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
    public class ProjectTimesheetsActionModel
    {
        private List<InvoiceStatusType> _invoiceStatusses;
        public List<EditTimesheetModel> Timesheets { get; set; }
        
        public Guid ProjectId { get; set; }

        public IEnumerable<SelectListItem> CreateInvoiceStatusSelectListItem(int statusCode)
        {
            if (_invoiceStatusses == null)
                _invoiceStatusses = Enum.GetValues(typeof(InvoiceStatusType)).OfType<InvoiceStatusType>().ToList();

            yield return CreateSelectListItem((InvoiceStatusType)statusCode, true);

            switch ((InvoiceStatusType)statusCode)
            {
                case InvoiceStatusType.Draft:
                    yield return CreateSelectListItem(InvoiceStatusType.ToVerify);
                    break;

                case InvoiceStatusType.ToVerify:
                    yield return CreateSelectListItem(InvoiceStatusType.Draft);
                    yield return CreateSelectListItem(InvoiceStatusType.ReadyForApproval);
                    yield return CreateSelectListItem(InvoiceStatusType.ReadyForInvoicing);
                    yield return CreateSelectListItem(InvoiceStatusType.ContestedByProma);
                    break;

                case InvoiceStatusType.ReadyForApproval:
                    yield return CreateSelectListItem(InvoiceStatusType.SentForApproval);
                    break;

                case InvoiceStatusType.ContestedByProma:
                    yield return CreateSelectListItem(InvoiceStatusType.Draft);
                    yield return CreateSelectListItem(InvoiceStatusType.ToVerify);
                    break;

                case InvoiceStatusType.ReadyForInvoicing:
                    yield return CreateSelectListItem(InvoiceStatusType.Invoiced);
                    break;

                case InvoiceStatusType.SentForApproval:
                    yield return CreateSelectListItem(InvoiceStatusType.ReadyForInvoicing);
                    yield return CreateSelectListItem(InvoiceStatusType.ContestedByCustomer);
                    break;

                case InvoiceStatusType.ContestedByCustomer:
                    yield return CreateSelectListItem(InvoiceStatusType.ReadyForApproval);
                    yield return CreateSelectListItem(InvoiceStatusType.ToVerify);
                    break;

                case InvoiceStatusType.FixedPrice:
                    yield return CreateSelectListItem(InvoiceStatusType.ContestedByProma);
                    break;
            }
        }

        private SelectListItem CreateSelectListItem(InvoiceStatusType status, bool isSelected = false)
        {
            return new SelectListItem { Selected = isSelected, Text = EnumMemberNameAttribute.GetName(status), Value = ((int)status).ToString(CultureInfo.InvariantCulture) };
        }
    }
}