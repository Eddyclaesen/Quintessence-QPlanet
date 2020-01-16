using System.Data.Entity;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Inf;

namespace Quintessence.QService.Data.CommandContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceCommandContext : IInfCommandContext
    {
        public IDbSet<Language> Languages { get; set; }
        public IDbSet<Office> Offices { get; set; }
        public IDbSet<MailTemplate> MailTemplates { get; set; }
        public IDbSet<MailTemplateTranslation> MailTemplateTranslations { get; set; }
        public IDbSet<Job> Jobs { get; set; }
    }
}
