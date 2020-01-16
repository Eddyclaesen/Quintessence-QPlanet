using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Cam;
using Quintessence.QService.QueryModel.Wsm;

namespace Quintessence.QService.Business.QueryRepositories
{
    public class WorkspaceManagementQueryRepository : QueryRepositoryBase<IWsmQueryContext>, IWorkspaceManagementQueryRepository
    {
        public WorkspaceManagementQueryRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public UserProfileView RetrieveUserProfile(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var userProfile = context.UserProfiles
                            .Include(up => up.Emails)
                            .Include(up => up.Contacts)
                            .SingleOrDefault(up => up.Id == id);
                        return userProfile;
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
