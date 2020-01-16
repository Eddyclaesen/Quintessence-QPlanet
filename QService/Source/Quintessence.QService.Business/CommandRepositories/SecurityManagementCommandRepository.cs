using System;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Sec;

namespace Quintessence.QService.Business.CommandRepositories
{
    public class SecurityManagementCommandRepository : CommandRepositoryBase<ISecCommandContext>, ISecurityManagementCommandRepository
    {
        public SecurityManagementCommandRepository(IUnityContainer container)
            : base(container)
        {
        }

        public Guid NegociateAuthenticationToken(string username, string password)
        {
            using (DurationLog.Create())
            {
                try
                {
                    User user;
                    using (var context = CreateContext())
                    {
                        var passwordHash = HashPassword(password);
                        user = context
                            .Users
                            .SingleOrDefault(u => u.UserName == username && u.Password == passwordHash);
                    }

                    if (user == null)
                        throw new AuthenticationException("Username or password are invalid.");

                    if (user.ChangePassword)
                        throw new AuthenticationException("Your password is expired.");

                    var authenticationToken = new AuthenticationToken
                        {
                            Id = Guid.NewGuid(),
                            IssuedOn = DateTime.Now,
                            UserId = user.Id,
                            ValidFrom = DateTime.Now,
                            ValidTo = DateTime.Now.AddDays(90).Subtract(DateTime.Now.TimeOfDay)
                        };

                    using (var context = CreateContext())
                    {
                        context.AuthenticationTokens.Add(authenticationToken);
                        context.SaveChanges();
                    }

                    return authenticationToken.Id;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void RevokeAuthenticationTokens(Guid userId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var authenticationTokens = context

                            .AuthenticationTokens

                            .Where(at => at.UserId == userId && at.ValidTo > DateTime.Now);

                        authenticationTokens.ForEach(at => at.ValidTo = DateTime.Now);

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

        public void ChangePassword(Guid userId, string currentPassword, string newPassword, string confirmPassword)
        {
            using (DurationLog.Create())
            {
                try
                {
                    User user;
                    using (var context = CreateContext())
                    {
                        var currentPasswordHash = HashPassword(currentPassword);
                        user = context
                            .Users
                            .SingleOrDefault(u => u.Id == userId && u.Password == currentPasswordHash);

                        if (user == null)
                            throw new AuthenticationException("Username or password are invalid.");

                        if (newPassword != confirmPassword)
                            throw new AuthenticationException("New password and confirmed password are not the same.");

                        user.Password = HashPassword(newPassword);
                        user.ChangePassword = false;

                        context.SaveChanges();
                    }

                    RevokeAuthenticationTokens(user.Id);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void ResetPassword(Guid userId, string resetPassword)
        {
            using (DurationLog.Create())
            {
                try
                {
                    User user;
                    using (var context = CreateContext())
                    {
                        var currentPasswordHash = HashPassword(resetPassword);
                        user = context
                            .Users
                            .SingleOrDefault(u => u.Id == userId);

                        user.Password = currentPasswordHash;
                        user.ChangePassword = true;

                        context.SaveChanges();
                    }

                    RevokeAuthenticationTokens(user.Id);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public User RetrieveUser(string username, string password)
        {
            using (DurationLog.Create())
            {
                try
                {
                    User user;
                    using (var context = CreateContext())
                    {
                        var passwordHash = HashPassword(password);
                        user = context
                            .Users
                            .SingleOrDefault(u => u.UserName == username && u.Password == passwordHash);
                    }

                    if (user == null)
                        throw new AuthenticationException("Username or password are invalid.");

                    return user;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public User PrepareUser(string userName)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        if (context.Users.Any(u => u.UserName == userName))
                            Container.Resolve<ValidationContainer>().RegisterEntityValidationFaultEntry("Username already exists.", "userName");
                    }

                    var user = Prepare<User>();
                    user.UserName = userName;

                    return user;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public Employee RetrieveEmployee(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var employee = context.Employees.SingleOrDefault(e => e.Id == id);

                        return employee;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public Employee PrepareEmployee(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var employee = new Employee();
                    employee.Id = id;
                    return employee;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void SaveEmployee(Employee employee)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var state = context.Employees.Any(e => e.Id == employee.Id)
                                        ? EntityState.Modified
                                        : EntityState.Added;

                        context.Employees.Attach(employee);
                        context.Entry(employee).State = state;
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

        public void DeleteEmployee(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var employee = context.Employees.SingleOrDefault(e => e.Id == id);

                        if (employee == null)
                            return;

                        context.Employees.Remove(employee);
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

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(password)));
        }
    }
}
