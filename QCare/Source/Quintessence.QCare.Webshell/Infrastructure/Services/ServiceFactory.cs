using System;
using System.ServiceModel;
using System.Web;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;

namespace Quintessence.QCare.Webshell.Infrastructure.Services
{
    public static class ServiceFactory
    {
        public static TResult InvokeService<TService, TResult>(Func<TService, TResult> action)
        {
            if (!AuthenticationTokenId.HasValue)
            {
                using (var factory = new ChannelFactory<IAuthenticationCommandService>(typeof(IAuthenticationCommandService).Name))
                {
                    var channel = factory.CreateChannel();

                    var response = channel.NegociateAuthenticationToken("QCare", "$Quint123");
                    AuthenticationTokenId = response.AuthenticationTokenId;
                }
            }
            
            using (var factory = new ChannelFactory<TService>(typeof(TService).Name))
            {
                var proxy = factory.CreateChannel();
                using (new OperationContextScope(proxy as IContextChannel))
                {
                    var typedMessageHeader = new MessageHeader<string>(AuthenticationTokenId.Value.ToString());
                    var messageHeader = typedMessageHeader.GetUntypedHeader("authenticationTokenId", "security");
                    OperationContext.Current.OutgoingMessageHeaders.Add(messageHeader);

                    return action.Invoke(proxy);
                }
            }
        }

        public static void InvokeService<TService>(Action<TService> action)
        {
            if (!AuthenticationTokenId.HasValue)
            {
                using (var factory = new ChannelFactory<IAuthenticationCommandService>(typeof(IAuthenticationCommandService).Name))
                {
                    var channel = factory.CreateChannel();

                    var response = channel.NegociateAuthenticationToken("QCare", "$Quint123");
                    AuthenticationTokenId = response.AuthenticationTokenId;
                }
            }

            using (var factory = new ChannelFactory<TService>(typeof(TService).Name))
            {
                var proxy = factory.CreateChannel();
                using (new OperationContextScope(proxy as IContextChannel))
                {
                    var typedMessageHeader = new MessageHeader<string>(AuthenticationTokenId.Value.ToString());
                    var messageHeader = typedMessageHeader.GetUntypedHeader("authenticationTokenId", "security");
                    OperationContext.Current.OutgoingMessageHeaders.Add(messageHeader);

                    action.Invoke(proxy);
                }
            }
        }

        private static Guid? AuthenticationTokenId
        {
            get { return HttpContext.Current.Session["AuthenticationTokenId"] as Guid?; }
            set { HttpContext.Current.Session["AuthenticationTokenId"] = value; }
        }
    }
}