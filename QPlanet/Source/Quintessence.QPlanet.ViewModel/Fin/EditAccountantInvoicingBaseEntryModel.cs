using System.Collections.Generic;
using System.Web.Mvc;

namespace Quintessence.QPlanet.ViewModel.Fin
{
    public class EditAccountantInvoicingBaseEntryModel : EditInvoicingBaseEntryModel
    {
        public string CrmProjectName { get; set; }

        public override List<SelectListItem> CreateInvoiceStatusDropDownList(int invoiceStatusCode)
        {
            throw new System.NotImplementedException();
        }
    }
}