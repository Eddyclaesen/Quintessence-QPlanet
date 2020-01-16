using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProposalView : ViewEntityBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string ContactDepartment { get; set; }

        public string ContactFullName
        {
            get
            {
                return string.IsNullOrWhiteSpace(ContactDepartment)
                    ? ContactName
                    : string.Format("{0} ({1})", ContactName, ContactDepartment);
            }
        }

        [DataMember]
        public Guid? BusinessDeveloperId { get; set; }

        [DataMember]
        public string BusinessDeveloperFirstName { get; set; }

        [DataMember]
        public string BusinessDeveloperLastName { get; set; }

        public string BusinessDeveloperFullName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(BusinessDeveloperFirstName) && !string.IsNullOrWhiteSpace(BusinessDeveloperLastName))
                    return string.Format("{0} {1}", BusinessDeveloperFirstName, BusinessDeveloperLastName);
                return string.Empty;
            }
        }

        [DataMember]
        public Guid? ExecutorId { get; set; }

        [DataMember]
        public string ExecutorFirstName { get; set; }

        [DataMember]
        public string ExecutorLastName { get; set; }

        public string ExecutorFullName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ExecutorFirstName) && !string.IsNullOrWhiteSpace(ExecutorLastName))
                    return string.Format("{0} {1}", ExecutorFirstName, ExecutorLastName);
                return string.Empty;
            }
        }

        [DataMember]
        public DateTime? DateReceived { get; set; }

        [DataMember]
        public DateTime? Deadline { get; set; }

        [DataMember]
        public DateTime? DateSent { get; set; }

        [DataMember]
        public DateTime? DateWon { get; set; }

        [DataMember]
        public decimal? PriceEstimation { get; set; }

        [DataMember]
        public decimal? Prognosis { get; set; }

        [DataMember]
        public decimal? FinalBudget { get; set; }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember]
        public string StatusReason { get; set; }

        public decimal? Total
        {
            get { return Prognosis.HasValue && PriceEstimation.HasValue ? Prognosis.Value * PriceEstimation.Value : (decimal?)null; }
        }

        [DataMember]
        public bool WrittenProposal { get; set; }
    }
}