using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QService.Data.Interfaces.QueryContext
{
    /// <summary>
    /// Interface for the Dictionairy Management data context
    /// </summary>
    public interface IInfQueryContext : IQuintessenceQueryContext
    {
        DbQuery<LanguageView> Languages { get; }
        DbQuery<OfficeView> Offices { get; }
        DbQuery<AssessmentRoomView> AssessmentRooms { get; }
        DbQuery<AssessorColorView> AssessorColors { get; }
        DbQuery<MailTemplateView> MailTemplates { get; }
        DbQuery<MailTemplateTranslationView> MailTemplateTranslations { get; }
        DbQuery<JobDefinitionView> JobDefinitions { get; }
        DbQuery<JobScheduleView> JobSchedules { get; }
        DbQuery<JobView> Jobs { get; }
        IEnumerable<MailTemplateTagView> ListMailTemplateTags(string storedProcedureName, Guid id);
        IEnumerable<MailTemplateTagView> ListMailTemplateTags(string storedProcedureName, Guid id, int languageId);
    }
}
