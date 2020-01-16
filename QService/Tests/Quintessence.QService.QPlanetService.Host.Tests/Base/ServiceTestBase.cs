using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;

namespace Quintessence.QService.QPlanetService.Host.Tests.Base
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected Guid TokenId { get; set; }

        [TestInitialize]
        public void InitializeTest()
        {
            using (var factory = new ChannelFactory<IAuthenticationCommandService>(typeof(IAuthenticationCommandService).Name))
            {
                var channel = factory.CreateChannel();

                var response = channel.NegociateAuthenticationToken("admin", "$Quint123");
                TokenId = response.AuthenticationTokenId;
            }
        }

        public void Execute<TService>(Action<TService> action)
        {
            using (var factory = new ChannelFactory<TService>(typeof(TService).Name))
            {
                var proxy = factory.CreateChannel();
                using (new OperationContextScope(proxy as IContextChannel))
                {
                    var typedMessageHeader = new MessageHeader<string>(TokenId.ToString());
                    var messageHeader = typedMessageHeader.GetUntypedHeader("authenticationTokenId", "security");
                    OperationContext.Current.OutgoingMessageHeaders.Add(messageHeader);

                    action.Invoke(proxy);
                }
            }
        }

        public TReturn Execute<TService, TReturn>(Func<TService, TReturn> action)
        {
            using (var factory = new ChannelFactory<TService>(typeof(TService).Name))
            {
                var proxy = factory.CreateChannel();
                using (new OperationContextScope(proxy as IContextChannel))
                {
                    var typedMessageHeader = new MessageHeader<string>(TokenId.ToString());
                    var messageHeader = typedMessageHeader.GetUntypedHeader("authenticationTokenId", "security");
                    OperationContext.Current.OutgoingMessageHeaders.Add(messageHeader);

                    return action.Invoke(proxy);
                }
            }
        }
    }
}
