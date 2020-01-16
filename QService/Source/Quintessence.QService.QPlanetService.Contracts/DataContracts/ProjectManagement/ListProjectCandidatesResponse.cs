using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class ListProjectCandidatesResponse
    {
        [DataMember]
        public List<ProjectCandidateView> Candidates { get; set; }

        [DataMember]
        public ProjectView Project { get; set; }
    }
}