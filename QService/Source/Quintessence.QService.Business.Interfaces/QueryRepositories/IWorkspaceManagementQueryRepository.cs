using System;
using Quintessence.QService.QueryModel.Wsm;

namespace Quintessence.QService.Business.Interfaces.QueryRepositories
{
    public interface IWorkspaceManagementQueryRepository : IQueryRepository
    {
        UserProfileView RetrieveUserProfile(Guid id);
    }
}