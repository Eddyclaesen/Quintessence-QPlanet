using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class BaseEditInvoiceModel : BaseEntityModel
    {
        public string ProductName { get; set; }
        public int InvoiceStatusCode { get; set; }
        public decimal InvoiceAmount { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoicedDate { get; set; }
        public string InvoiceRemarks { get; set; }
        public string DetailType { get { return GetType().FullName; } }

        public IEnumerable<SelectListItem> CreateInvoiceStatusSelectListItems(int invoiceStatusCode, InvoiceStatusType currentType)
        {
                return Enum.GetValues(typeof(InvoiceStatusType))
                    .OfType<InvoiceStatusType>()
                    .Except(new[] { InvoiceStatusType.Invoiced })
                    .Select(invoiceStatusType =>
                                                    new SelectListItem
                                                    {
                                                        Value = ((int)invoiceStatusType).ToString(),
                                                        Text = EnumMemberNameAttribute.GetName(invoiceStatusType),
                                                        Selected = (int)invoiceStatusType == invoiceStatusCode
                                                    });
            }
    }
}