using System;
using System.ServiceModel;
using System.Transactions;
using Microsoft.Practices.Unity;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Core.Testing;
using Quintessence.QService.Core.Unity;

namespace Quintessence.QService.QPlanetService.Implementation.Base
{
    public abstract class UnityServiceBase
    {
        private IUnityContainer _container;

        protected UnityServiceBase(IUnityContainer container = null)
        {
            _container = container;
        }

        public virtual IUnityContainer Container
        {
            protected get
            {
                return OperationContext.Current == null
                    ? (_container ?? (_container = StaticUnityContainer.UnityContainer)) //When not in a wcf context
                    : ((UnityServiceHost)OperationContext.Current.Host).Container;
            }
            set { _container = value; }
        }

        public virtual TResult ExecuteTransaction<TResult>(Func<TResult> func)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var result = func.Invoke();

                    //Check for errors
                    ValidationContainer.Validate();
                    RemoveValidationContainer();

                    ts.Complete();
                    return result;
                }
                catch (FaultException<ValidationContainer>)
                {
                    RemoveValidationContainer();
                    throw;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    ValidationContainer.RegisterException(exception);
                    throw new FaultException<ValidationContainer>(RemoveValidationContainer(), exception.Message);
                }
            }
        }

        private ValidationContainer RemoveValidationContainer()
        {
            var validationContainer = ValidationContainer;

            Container.Resolve<ValidationContextLifetimeManager>().RemoveValue();

            return validationContainer;
        }

        public virtual void ExecuteTransaction(Action action)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    action.Invoke();

                    //Check for errors
                    ValidationContainer.Validate();
                    RemoveValidationContainer();

                    ts.Complete();
                }
                catch (FaultException<ValidationContainer>)
                {
                    RemoveValidationContainer();
                    throw;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    ValidationContainer.RegisterException(exception);
                    throw new FaultException<ValidationContainer>(RemoveValidationContainer(), exception.Message);
                }
            }
        }

        public virtual TResult Execute<TResult>(Func<TResult> func)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Suppress))
            {
                try
                {
                    var result = func.Invoke();

                    //Check for errors
                    ValidationContainer.Validate();
                    RemoveValidationContainer();

                    ts.Complete();

                    return result;
                }
                catch (FaultException<ValidationContainer>)
                {
                    RemoveValidationContainer();
                    throw;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    ValidationContainer.RegisterException(exception);
                    throw new FaultException<ValidationContainer>(RemoveValidationContainer(), exception.Message);
                }
            }
        }

        public virtual void Execute(Action action)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Suppress))
            {
                try
                {
                    action.Invoke();

                    //Check for errors
                    ValidationContainer.Validate();
                    RemoveValidationContainer();

                    ts.Complete();
                }
                catch (FaultException<ValidationContainer>)
                {
                    RemoveValidationContainer();
                    throw;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    ValidationContainer.RegisterException(exception);
                    throw new FaultException<ValidationContainer>(RemoveValidationContainer(), exception.Message);
                }
            }
        }

        protected ValidationContainer ValidationContainer
        {
            get { return Container.Resolve<ValidationContainer>(); }
        }
    }
}
