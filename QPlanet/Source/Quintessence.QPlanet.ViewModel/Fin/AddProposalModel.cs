using System;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Fin
{
    public class AddProposalModel : BaseEntityModel
    {     
        public string ProjectName { get; set; }
        public string CandidateFullName { get; set; }
        public string ProductName { get; set; }
        public int ContactId { get; set; }
        public decimal InvoiceAmount { get; set; }
    }
}