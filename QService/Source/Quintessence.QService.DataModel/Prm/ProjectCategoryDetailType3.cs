namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectCategoryDetailType3 : ProjectCategoryDetail
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int SurveyPlanningId { get; set; }
        public string MailTextStandalone { get; set; }
        public string MailTextIntegrated { get; set; }
        public bool IncludeInCandidateReport { get; set; }
    }
}
