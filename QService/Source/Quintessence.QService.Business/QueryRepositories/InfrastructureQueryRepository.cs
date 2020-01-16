using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Base;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QService.Business.QueryRepositories
{
    public class InfrastructureQueryRepository : QueryRepositoryBase<IInfQueryContext>, IInfrastructureQueryRepository
    {
        public InfrastructureQueryRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public List<LanguageView> ListLanguages()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var languages = context.Languages.ToList();

                        return languages;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<AssessmentRoomView> ListAssessmentRooms()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var assessmentRooms = context.AssessmentRooms.ToList();

                        return assessmentRooms;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public OfficeView RetrieveOffice(int id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var office = context.Offices.SingleOrDefault(o => o.Id == id);

                        return office;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public OfficeView RetrieveOffice(string shortName)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var office = context.Offices.SingleOrDefault(o => o.ShortName == shortName);

                        return office;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<AssessmentRoomView> ListOfficeAssessmentRooms(int officeId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var assessmentRooms = context.AssessmentRooms
                            .Where(ar => ar.OfficeId == officeId)
                            .ToList();

                        return assessmentRooms;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public MailTemplateView RetrieveMailTemplateByCode(string code)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var mailTemplate = context.MailTemplates
                                                  .Include(mt => mt.MailTemplateTranslations)
                                                  .SingleOrDefault(mt => mt.Code == code);

                        return mailTemplate;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public MailTemplateView RetrieveMailTemplate(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var mailTemplate = context.MailTemplates
                                                  .Include(mt => mt.MailTemplateTranslations)
                                                  .SingleOrDefault(mt => mt.Id == id);

                        return mailTemplate;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<MailTemplateTagView> ListMailTemplateTags(string storedProcedureName, Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var mailTemplateTags = context.ListMailTemplateTags(storedProcedureName, id)
                                                    .ToList();

                        return mailTemplateTags;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<MailTemplateTagView> ListMailTemplateTags(string storedProcedureName, Guid id, int languageId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var mailTemplateTags = context.ListMailTemplateTags(storedProcedureName, id, languageId)
                                                    .ToList();

                        return mailTemplateTags;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<JobDefinitionView> ListJobDefinitions()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var jobDefinitions = context.JobDefinitions
                            .Include(jd => jd.JobSchedules)
                            .ToList();

                        return jobDefinitions;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }
    }
}
