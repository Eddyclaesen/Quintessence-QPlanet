using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Fin
{
    public abstract class EditInvoicingBaseEntryModel : BaseEntityModel
    {
        public Guid ProjectId { get; set; }

        public bool IsDiry { get; set; }

        public string ProjectName { get; set; }

        public int ContactId { get; set; }

        public string ContactName { get; set; }

        public DateTime? InvoicedDate { get; set; }

        public decimal? InvoiceAmount { get; set; }

        public string InvoiceRemarks { get; set; }

        public int InvoiceStatusCode { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string InvoiceNumber { get; set; }

        public string ProductName { get; set; }

        public string CustomerAssistantFirstName { get; set; }

        public string CustomerAssistantLastName { get; set; }

        public string CustomerAssistantFullName { get { return CustomerAssistantFirstName + ' ' + CustomerAssistantLastName; } }

        public string CustomerAssistantUserName { get; set; }

        public string ProjectManagerFirstName { get; set; }

        public string ProjectManagerLastName { get; set; }

        public string ProjectManagerFullName { get { return ProjectManagerFirstName + ' ' + ProjectManagerLastName; } }

        public string ProjectManagerUserName { get; set; }

        public string ConsultantFirstName { get; set; }

        public string ConsultantLastName { get; set; }

        public string ConsultantFullName { get { return ConsultantFirstName + ' ' + ConsultantLastName; } }

        public string ConsultantUserName { get; set; }

        public abstract List<SelectListItem> CreateInvoiceStatusDropDownList(int invoiceStatusCode);

        public string DetailType { get { return GetType().FullName; } }

        public Guid? ProposalId { get; set; }
    }
}