using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Extensions;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Cam;

namespace Quintessence.QService.Business.CommandRepositories
{
    public class CandidateManagementCommandRepository : CommandRepositoryBase<ICamCommandContext>, ICandidateManagementCommandRepository
    {
        public CandidateManagementCommandRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public void DeleteProjectCandidateProgramComponents(Guid projectCandidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var programComponents = context.ProgramComponents
                            .Where(pc => pc.ProjectCandidateId == projectCandidateId)
                            .ToList();

                        foreach (var programComponent in programComponents)
                            context.Delete(programComponent);

                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteRoomProgramComponents(Guid roomId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var programComponents = context.ProgramComponents
                            .Where(pc => pc.AssessmentRoomId == roomId)
                            .ToList();

                        foreach (var programComponent in programComponents)
                            context.Delete(programComponent);

                        context.SaveChanges();
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
