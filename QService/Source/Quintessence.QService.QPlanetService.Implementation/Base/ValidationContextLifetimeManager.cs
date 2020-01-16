using System;
using System.Collections.Generic;
using System.ServiceModel;
using Microsoft.Practices.Unity;
using Microsoft.Practices;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.Core.Logging;

namespace Quintessence.QService.QPlanetService.Implementation.Base
{
    public class ValidationContextLifetimeManager : LifetimeManager
    {
        private readonly Dictionary<string, ValidationContainer> _validationContexts = new Dictionary<string, ValidationContainer>();
        private static readonly object Stone = new object();

        public override object GetValue()
        {
            if (!_validationContexts.ContainsKey(OperationContext.Current.SessionId))
                lock (Stone)
                    if (!_validationContexts.ContainsKey(OperationContext.Current.SessionId))
                        _validationContexts.Add(OperationContext.Current.SessionId, Activator.CreateInstance<ValidationContainer>());
            LogManager.LogTrace(string.Format("System;Amount of ValidationContext in ValidationContextLifetimeManager: {0}", _validationContexts.Count));
            return _validationContexts[OperationContext.Current.SessionId];
        }

        public override void SetValue(object newValue)
        {
            lock (Stone)
                _validationContexts[OperationContext.Current.SessionId] = (ValidationContainer)newValue;
        }

        public override void RemoveValue()
        {
            if (_validationContexts.ContainsKey(OperationContext.Current.SessionId))
                lock (Stone)
                    if (_validationContexts.ContainsKey(OperationContext.Current.SessionId))
                        _validationContexts.Remove(OperationContext.Current.SessionId);
        }
    }
}
