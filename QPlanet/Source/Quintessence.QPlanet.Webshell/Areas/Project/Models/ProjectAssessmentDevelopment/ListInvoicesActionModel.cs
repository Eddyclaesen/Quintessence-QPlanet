using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectAssessmentDevelopment
{
    public class ListInvoicesActionModel
    {
        public List<BaseEditInvoiceModel> ToVerifyList { get; set; }
        public List<BaseEditInvoiceModel> ReadyForInvoicingList { get; set; }
        public List<BaseEditInvoiceModel> InvoicedList { get; set; }
        public List<BaseEditInvoiceModel> PlannedList { get; set; }

        public AssessmentDevelopmentProjectView Project { get; set; }

        
    }
}