using System;
using Quintessence.Infrastructure.Model.DataModel;
using Quintessence.QService.DataModel.Sec;

namespace Quintessence.QService.Business.Interfaces.CommandRepositories
{
    public interface ISecurityManagementCommandRepository : ICommandRepository
    {
        Guid NegociateAuthenticationToken(string username, string password);
        void RevokeAuthenticationTokens(Guid userId);
        void ChangePassword(Guid userId, string currentPassword, string newPassword, string confirmPassword);
        void ResetPassword(Guid userId, string resetPassword);
        User RetrieveUser(string username, string password);
        User PrepareUser(string userName);
        Employee RetrieveEmployee(Guid id);
        Employee PrepareEmployee(Guid id);
        void SaveEmployee(Employee employee);
        void DeleteEmployee(Guid id);
    }
}
