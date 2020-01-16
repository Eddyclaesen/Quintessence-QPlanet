using System;
using System.Runtime.Serialization;
using Quintessence.CulturalFit.Service.Contracts.DataContracts.Base;

namespace Quintessence.CulturalFit.Service.Contracts.DataContracts
{
    [DataContract]
    public class CandidateRequest: BaseRequest
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the CRM participant id.
        /// </summary>
        /// <value>
        /// The CRM participant id.
        /// </value>
        [DataMember]
        public int CrmParticipantId { get; set; }

    }
}
