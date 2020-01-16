using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class ListProjectCategoryDetailDictionaryIndicatorsRequest
    {
        public ListProjectCategoryDetailDictionaryIndicatorsRequest() { }

        public ListProjectCategoryDetailDictionaryIndicatorsRequest(Guid projectCategoryDetailId, int? languageId = null)
            : this()
        {
            ProjectCategoryDetailId = projectCategoryDetailId;
            LanguageId = languageId;
        }

        [DataMember]
        public Guid ProjectCategoryDetailId { get; set; }

        [DataMember]
        public int? LanguageId { get; set; }
    }
}