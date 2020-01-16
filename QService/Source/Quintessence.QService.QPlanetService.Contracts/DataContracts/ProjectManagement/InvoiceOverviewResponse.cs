using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class InvoiceOverviewResponse
    {
        [DataMember]
        public List<ProjectCandidateView> ProjectCandidates { get; set; }

        [DataMember]
        public List<ProjectProductView> ProjectProducts { get; set; }

        [DataMember]
        public AssessmentDevelopmentProjectView Project { get; set; }
    }
}