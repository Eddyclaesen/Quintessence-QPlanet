using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QService.Data.QueryContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceQueryContext : IInfQueryContext
    {
        public DbQuery<LanguageView> Languages { get { return Set<LanguageView>().AsNoTracking(); } }
        public DbQuery<OfficeView> Offices { get { return Set<OfficeView>().AsNoTracking(); } }
        public DbQuery<AssessmentRoomView> AssessmentRooms { get { return Set<AssessmentRoomView>().AsNoTracking(); } }
        public DbQuery<AssessorColorView> AssessorColors { get { return Set<AssessorColorView>().AsNoTracking(); } }
        public DbQuery<MailTemplateView> MailTemplates { get { return Set<MailTemplateView>().AsNoTracking(); } }
        public DbQuery<MailTemplateTranslationView> MailTemplateTranslations { get { return Set<MailTemplateTranslationView>().AsNoTracking(); } }
        public DbQuery<JobDefinitionView> JobDefinitions { get { return Set<JobDefinitionView>().AsNoTracking(); } }
        public DbQuery<JobScheduleView> JobSchedules { get { return Set<JobScheduleView>().AsNoTracking(); } }
        public DbQuery<JobView> Jobs { get { return Set<JobView>().AsNoTracking(); } }

        public IEnumerable<MailTemplateTagView> ListMailTemplateTags(string storedProcedureName, Guid id)
        {
            var query = Database.SqlQuery<MailTemplateTagView>(storedProcedureName + " {0}", id);
            return query.ToList();
        }

        public IEnumerable<MailTemplateTagView> ListMailTemplateTags(string storedProcedureName, Guid id, int languageId)
        {
            var query = Database.SqlQuery<MailTemplateTagView>(storedProcedureName + " {0}, {1}", id, languageId);
            return query.ToList();
        }
    }
}
