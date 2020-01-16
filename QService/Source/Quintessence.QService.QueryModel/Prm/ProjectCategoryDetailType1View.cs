using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCategoryDetailType1View : ProjectCategoryDetailView
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int SurveyPlanningId { get; set; }

        [DataMember]
        public string MailTextStandalone { get; set; }

        [DataMember]
        public string MailTextIntegrated { get; set; }
    }
}