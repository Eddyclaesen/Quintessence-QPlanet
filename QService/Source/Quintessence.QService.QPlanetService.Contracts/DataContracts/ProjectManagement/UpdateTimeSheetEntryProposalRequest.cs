using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateTimeSheetEntryProposalRequest
    {
        [DataMember]
        public Guid TimeSheetEntryId { get; set; }

        [DataMember]
        public Guid ProposalId { get; set; }
    }
}