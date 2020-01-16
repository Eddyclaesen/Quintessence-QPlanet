using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QService.Business.QueryRepositories
{
    public class ReportManagementQueryRepository : QueryRepositoryBase<IRepQueryContext>, IReportManagementQueryRepository
    {
        public ReportManagementQueryRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public List<CandidateReportDefinitionView> ListCandidateReportDefinitions()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var definitions = context.CandidateReportDefinitions
                            .Include(crd => crd.CandidateReportDefinitionFields)
                            .ToList();
                        return definitions;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<CandidateReportDefinitionView> ListCandidateReportDefinitionsForCustomer(int contactId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var definitions = context.CandidateReportDefinitions
                            .Include(crd => crd.CandidateReportDefinitionFields)
                            .Where(crd => (!crd.ContactId.HasValue || crd.ContactId == contactId))
                            .ToList();
                        return definitions;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<CandidateScoreReportTypeView> ListCandidateScoreReportTypes()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var reportTypes = context.CandidateScoreReportTypes
                            .ToList();
                        return reportTypes;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public CandidateReportDefinitionView RetrieveCandidateReportDefinition(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var candidateReportDefinition = context.CandidateReportDefinitions
                            .Include(crd => crd.CandidateReportDefinitionFields)
                            .SingleOrDefault(crd => crd.Id == id);
                        return candidateReportDefinition;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ReportTypeView> ListReportTypes()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var reportTypes = context.ReportTypes.ToList();
                        return reportTypes;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ReportDefinitionView> ListReportDefinitionsByReportType(int reportTypeId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var reportDefinitions = context.ReportDefinitions
                            .Where(rd => rd.ReportTypeId == reportTypeId)
                            .ToList();
                        return reportDefinitions;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ReportDefinitionView> ListReportDefinitions()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var reportDefinitions = context.ReportDefinitions.ToList();
                        return reportDefinitions;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ReportParameterView RetrieveReportParameter(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var reportParameter = context.ReportParameters
                            .Include(rp => rp.ReportParameterValues)
                            .SingleOrDefault(rp => rp.Id == id);

                        return reportParameter;
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
