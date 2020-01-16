using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.ViewModel.Fin
{
    public class EditAccountantAcdcProjectFixedPriceInvoicingEntryModel : EditAccountantInvoicingBaseEntryModel
    {
        public DateTime Date { get; set; }

        public override List<SelectListItem> CreateInvoiceStatusDropDownList(int invoiceStatusCode)
        {
            return Enum.GetValues(typeof(InvoiceStatusType))
                .OfType<InvoiceStatusType>()
                .Select(i => new SelectListItem
                {
                    Value = ((int)i).ToString(CultureInfo.InvariantCulture),
                    Text = EnumMemberNameAttribute.GetName(i),
                    Selected = (int)i == invoiceStatusCode
                })
                .ToList();
        }
    }
}