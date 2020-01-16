using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectEvaluation : EntityBase
    {
        public int CrmProjectId { get; set; }
        public string LessonsLearned { get; set; }
        public string Evaluation { get; set; }
    }
}