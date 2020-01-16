using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Cam
{
    public class ProgramComponentSpecial : EntityBase
    {
        public string Name { get; set; }
        public int Execution { get; set; }
        public bool IsWithLeadAssessor { get; set; }
    }
}