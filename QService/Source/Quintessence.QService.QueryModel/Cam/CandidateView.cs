using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.QueryModel.Cam
{
    [DataContract]
    public class CandidateView : ViewEntityBase
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string LanguageName { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string Reference { get; set; }

        [DataMember]
        public List<ProjectCandidateView> ProjectCandidates { get; set; }
        [DataMember]
        public bool HasQCandidateAccess { get; set; }
        [DataMember]
        public Guid? QCandidateUserId { get; set; }
    }
}
