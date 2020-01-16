using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Fin
{
    [DataContract(IsReference = true)]
    public class AccountantInvoicingProjectCandidateCategoryType3EntryView : AccountantInvoicingBaseEntryView
    {
        [DataMember]
        public string CandidateFirstName { get; set; }

        [DataMember]
        public string CandidateLastName { get; set; }

        public string CandidateFullName
        {
            get { return string.Format("{0} {1}", CandidateFirstName, CandidateLastName); }
        }

        [DataMember]
        public DateTime? Date { get; set; }
    }
}