using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Quintessence.QService.QPlanetService.Implementation.Base.Behaviors
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SecureOperationBehaviorAttribute : Attribute, IOperationBehavior
    {
        private readonly string _domain;
        private readonly string _operation;

        public SecureOperationBehaviorAttribute(string domain, string operation)
        {
            _domain = domain;
            _operation = operation;
        }

        public void Validate(OperationDescription operationDescription)
        {
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.Invoker = new SecureOperationInvoker(dispatchOperation.Invoker, _domain, _operation);
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
        }

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }
    }
}