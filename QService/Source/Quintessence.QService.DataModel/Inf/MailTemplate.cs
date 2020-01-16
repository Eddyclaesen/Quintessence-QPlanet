using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Inf
{
    public class MailTemplate : EntityBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string FromAddress { get; set; }
        public string BccAddress { get; set; }
        public string StoredProcedureName { get; set; }
    }
}