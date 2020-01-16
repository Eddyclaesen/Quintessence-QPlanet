using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class ProjectTypeCategoryUnitPriceOverviewResponse
    {
        [DataMember]
        public List<ProjectTypeCategoryView> ProjectTypeCategories { get; set; }

        [DataMember]
        public List<ProjectTypeCategoryLevelView> ProjectTypeCategoryLevels { get; set; }

        [DataMember]
        public List<ProjectTypeCategoryUnitPriceView> ProjectTypeCategoryUnitPrices { get; set; }
    }
}