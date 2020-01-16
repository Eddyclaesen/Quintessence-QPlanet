using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProductSheetEntryProposalRequest
    {
        [DataMember]
        public Guid ProductSheetEntryId { get; set; }

        [DataMember]
        public Guid ProposalId { get; set; }
    }
}