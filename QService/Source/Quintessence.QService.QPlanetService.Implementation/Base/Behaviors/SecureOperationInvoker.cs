using System;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.Unity;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.Core.Security;
using Quintessence.QService.Core.Testing;
using Quintessence.QService.Core.Unity;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;

namespace Quintessence.QService.QPlanetService.Implementation.Base.Behaviors
{
    public class SecureOperationInvoker : IOperationInvoker
    {
        private IUnityContainer _container;
        private readonly IOperationInvoker _originalInvoker;
        private readonly string _domain;
        private readonly string _operation;

        public SecureOperationInvoker(IOperationInvoker originalInvoker, string domain, string operation)
        {
            _originalInvoker = originalInvoker;
            _domain = domain;
            _operation = operation;
        }

        public IUnityContainer Container
        {
            get
            {
                return OperationContext.Current == null
                    ? (_container ?? (_container = StaticUnityContainer.UnityContainer)) //When not in a wcf context
                    : ((UnityServiceHost)OperationContext.Current.Host).Container;
            }
        }

        public object[] AllocateInputs()
        {
            return _originalInvoker.AllocateInputs();
        }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            var securedUnityServiceBase = instance as SecuredUnityServiceBase;

            if (securedUnityServiceBase != null)
            {
                var securityContext = Container.Resolve<SecurityContext>();

                if (securityContext == null)
                    throw new Exception();

                var tokenId = securityContext.TokenId;

                var authenticationQueryService = Container.Resolve<IAuthenticationQueryService>();

                if (!authenticationQueryService.VerifyOperation(tokenId, _domain, _operation))
                    Container.Resolve<ValidationContainer>().RegisterAuthenticationFaultEntry(string.Format("Unautherized operation: {0} (domain: {1}). Please supply other credentials or contact the system administrator for access.", _operation, _domain));
            }

            return _originalInvoker.Invoke(instance, inputs, out outputs);
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            return _originalInvoker.InvokeBegin(instance, inputs, callback, state);
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            return _originalInvoker.InvokeEnd(instance, out outputs, result);
        }

        public bool IsSynchronous
        {
            get { return _originalInvoker.IsSynchronous; }
        }
    }
}
