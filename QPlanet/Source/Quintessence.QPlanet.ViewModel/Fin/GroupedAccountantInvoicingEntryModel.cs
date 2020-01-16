using System.Collections.Generic;

namespace Quintessence.QPlanet.ViewModel.Fin
{
    public class GroupedAccountantInvoicingEntryModel
    {
        public string CrmProjectName { get; set; }
        public List<EditAccountantInvoicingBaseEntryModel> InvoicingEntries { get; set; }  
    }
}