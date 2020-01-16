using System.Collections.Generic;
using System.Linq;
using Quintessence.QPlanet.ViewModel.Fin;

namespace Quintessence.QPlanet.Webshell.Areas.Finance.Models.FinanceHome
{
    public class ListAccountantInvoicingModel
    {
        public List<GroupedAccountantInvoicingEntryModel> GroupedInvoicingEntries { get; set; }
        
    }
}