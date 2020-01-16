using System;
using System.ServiceModel;
using System.Web;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Infrastructure.Security;

namespace Quintessence.QPlanet.Infrastructure.Services
{
    public class ServiceInvoker<TService>
    {

        public void Execute(HttpContext context, Action<TService> action)
        {
            try
            {
                LogManager.LogTrace("Creating factory {0}.", typeof(TService).Name);
                using (var factory = new ChannelFactory<TService>(typeof(TService).Name))
                {
                    LogManager.LogTrace("Creating channel for service {0}", typeof(TService).Name);
                    var proxy = factory.CreateChannel();
                    using (new OperationContextScope(proxy as IContextChannel))
                    {
                        LogManager.LogTrace("Retrieving identity.");
                        var identity = IdentityHelper.RetrieveIdentity(context);

                        if (identity != null)
                        {
                            LogManager.LogTrace("Identity found.");
                            var typedMessageHeader = new MessageHeader<string>(identity.Ticket.UserData);
                            var messageHeader = typedMessageHeader.GetUntypedHeader("authenticationTokenId", "security");
                            OperationContext.Current.OutgoingMessageHeaders.Add(messageHeader);
                        }
                        else
                        {
                            LogManager.LogTrace("No identity found.");
                        }

                        LogManager.LogTrace("Invoking procy {0}.", typeof(TService).Name);
                        action.Invoke(proxy);
                    }
                }
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                throw;
            }
        }

        public TReturn Execute<TReturn>(HttpContext context, Func<TService, TReturn> action)
        {
            try
            {
                LogManager.LogTrace("Creating factory {0}.", typeof(TService).Name);
                using (var factory = new ChannelFactory<TService>(typeof(TService).Name))
                {
                    LogManager.LogTrace("Creating channel for service {0}", typeof(TService).Name);
                    var proxy = factory.CreateChannel();
                    using (new OperationContextScope(proxy as IContextChannel))
                    {
                        LogManager.LogTrace("Retrieving identity.");
                        var identity = IdentityHelper.RetrieveIdentity(context);

                        if (identity != null)
                        {
                            LogManager.LogTrace("Identity found.");
                            var typedMessageHeader = new MessageHeader<string>(identity.Ticket.UserData);
                            var messageHeader = typedMessageHeader.GetUntypedHeader("authenticationTokenId", "security");
                            OperationContext.Current.OutgoingMessageHeaders.Add(messageHeader);
                        }
                        else
                        {
                            LogManager.LogTrace("No identity found.");
                        }

                        LogManager.LogTrace("Invoking proxy {0}.", typeof(TService).Name);
                        return action.Invoke(proxy);
                    }
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