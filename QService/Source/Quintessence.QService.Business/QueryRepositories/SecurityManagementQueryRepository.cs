using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QService.Business.QueryRepositories
{
    public class SecurityManagementQueryRepository : QueryRepositoryBase<ISecQueryContext>, ISecurityManagementQueryRepository
    {
        public SecurityManagementQueryRepository(IUnityContainer container)
            : base(container)
        {
        }

        public AuthenticationTokenView RetrieveAuthenticationTokenDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                            var token = context.AuthenticationTokens
                            .Include(at => at.User)
                            .SingleOrDefault(at => at.Id == id);

                        if (token == null)
                            throw new UnauthorizedAccessException();

                        return token;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public AuthenticationTokenView RetrieveAuthenticationToken(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var token = context
                            .AuthenticationTokens
                            .SingleOrDefault(at => at.Id == id);

                        return token;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public UserView RetrieveUserByCrmAssociateId(int? associateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var user = context
                            .Users
                            .SingleOrDefault(at => at.AssociateId == associateId);

                        return user;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public UserView RetrieveUser(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var user = context
                            .Users
                            .SingleOrDefault(at => at.Id == id);

                        return user;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<UserView> ListCustomerAssistants()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var customerAssistantRole = context.Roles.FirstOrDefault(r => r.Code == "CUSTA");
                        var users = context.Users
                                           .Where(u => u.RoleId == customerAssistantRole.Id)
                                           .ToList();

                        return users;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public RoleView RetrieveRole(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var role = context
                            .Roles
                            .SingleOrDefault(at => at.Id == id);

                        return role;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<UserView> ListUsersInRole(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var users = context.Users
                            .Where(u => u.RoleId == id)
                            .ToList();

                        return users;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<RoleOperationView> ListOperationsInRole(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var users = context.RoleOperations.ToList();

                        return users;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<UserView> SearchUsers(string name = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var users = context

                            .Users

                            .Where(u => (name == null || (u.FirstName + " " + u.LastName).Contains(name)))

                            .ToList();

                        return users;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public UserView RetrieveUserDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var user = context
                            .Users
                            .SingleOrDefault(u => u.Id == id);

                        return user;
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
