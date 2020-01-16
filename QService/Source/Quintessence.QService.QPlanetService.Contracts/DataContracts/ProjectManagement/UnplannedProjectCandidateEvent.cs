using System;
using System.Runtime.Serialization;
using System.Text;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UnplannedProjectCandidateEvent
    {
        [DataMember]
        public Guid ProjectCandidateId { get; set; }

        [DataMember]
        public Guid AssessmentRoomId { get; set; }

        [DataMember]
        public Guid? SimulationCombinationId { get; set; }

        [DataMember]
        public string SimulationSetName { get; set; }

        [DataMember]
        public string SimulationDepartmentName { get; set; }

        [DataMember]
        public string SimulationLevelName { get; set; }

        [DataMember]
        public string SimulationName { get; set; }

        public string SimulationCombinationName
        {
            get
            {
                var stringbuilder = new StringBuilder();

                stringbuilder.Append(SimulationSetName);

                if (!string.IsNullOrWhiteSpace(SimulationDepartmentName))
                    stringbuilder.AppendFormat(" - {0}", SimulationDepartmentName);

                if (!string.IsNullOrWhiteSpace(SimulationLevelName))
                    stringbuilder.AppendFormat(" - {0}", SimulationLevelName);

                stringbuilder.AppendFormat(" - {0}", SimulationName);

                return stringbuilder.ToString();
            }
        }

        [DataMember]
        public Guid? ProjectCandidateCategoryDetailTypeId { get; set; }

        [DataMember]
        public string ProjectCategoryDetailTypeName { get; set; }

        [DataMember]
        public string ProjectCandidateFullName { get; set; }

        [DataMember]
        public Guid? ProgramComponentSpecialId { get; set; }

        [DataMember]
        public string ProgramComponentSpecialName { get; set; }

        [DataMember]
        public string ContactName { get; set; }

    }
}