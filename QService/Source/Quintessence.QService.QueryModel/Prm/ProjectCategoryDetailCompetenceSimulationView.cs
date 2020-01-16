using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public class ProjectCategoryDetailCompetenceSimulationView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid ProjectCategoryDetailId { get; set; }

        [DataMember]
        public Guid DictionaryCompetenceId { get; set; }

        [DataMember]
        public Guid SimulationCombinationId { get; set; }
    }
}