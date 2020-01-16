using System.Data.Entity;
using Quintessence.QService.DataModel.Inf;

namespace Quintessence.QService.Data.Interfaces.CommandContext
{
    /// <summary>
    /// Interface for the Dictionairy Management data context
    /// </summary>
    public interface IInfCommandContext : IQuintessenceCommandContext
    {
        IDbSet<Language> Languages { get; set; }
        IDbSet<Office> Offices { get; set; }
        IDbSet<MailTemplate> MailTemplates { get; set; }
        IDbSet<MailTemplateTranslation> MailTemplateTranslations { get; set; }
        IDbSet<Job> Jobs { get; set; }
    }
}
