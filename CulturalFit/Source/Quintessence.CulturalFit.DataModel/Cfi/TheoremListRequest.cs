using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using Quintessence.CulturalFit.DataModel.Crm;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.DataModel.Cfi
{
    [DataContract(IsReference = true)]
    public class TheoremListRequest : IEntity
    {
        private List<TheoremList> _theoremLists;

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the contact id.
        /// </summary>
        /// <value>
        /// The contact id.
        /// </value>
        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the request date.
        /// </summary>
        /// <value>
        /// The request date.
        /// </value>
        [DataMember]
        public DateTime RequestDate { get; set; }

        [DataMember]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Deadline { get; set; }

        [DataMember]
        public string VerificationCode { get; set; }

        [DataMember]
        public bool IsMailSent { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DataMember]
        public int TheoremListRequestTypeId { get; set; }

        /// <summary>
        /// Gets or sets the theorem list request type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DataMember]
        public TheoremListRequestType TheoremListRequestType { get; set; }

        /// <summary>
        /// Gets or sets the theorem lists.
        /// </summary>
        /// <value>
        /// The theorem lists.
        /// </value>
        [DataMember]
        public List<TheoremList> TheoremLists
        {
            get { return _theoremLists ?? (_theoremLists = new List<TheoremList>()); }
            set { _theoremLists = value; }
        }

        [DataMember]
        public Contact Contact { get; set; }

        [DataMember]
        public Audit Audit { get; set; }

        public TheoremList AddTheoremList(TheoremList theoremList = null)
        {
            if (theoremList == null)
                theoremList = new TheoremList { Id = Guid.NewGuid() };

            TheoremLists.Add(theoremList);
            theoremList.TheoremListRequestId = Id;
            theoremList.TheoremListRequest = this;
            return theoremList;
        }

        [DataMember]
        public int? CrmEmailId { get; set; }

        [DataMember]
        public CrmReplicationEmail CrmEmail { get; set; }

        [DataMember]
        public Guid? CandidateId { get; set; }

        [DataMember]
        public Candidate Candidate { get; set; }
    }
}
