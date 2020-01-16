using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace Quintessence.Infrastructure.Validation
{
    public class ValidationRuleEngine
    {
        public ValidationRuleEngine(IUnityContainer unityContainer)
        {
            Container = unityContainer;
            ValidationRules = new Dictionary<Type, List<object>>();
        }

        protected IUnityContainer Container { get; private set; }
        protected Dictionary<Type, List<object>> ValidationRules { get; private set; }

        public void RegisterRule<T>(Func<IUnityContainer, T, bool> action)
        {
            if (!ValidationRules.ContainsKey(typeof(T)))
                ValidationRules[typeof(T)] = new List<object>();

            ValidationRules[typeof(T)].Add(action);
        }

        public bool Validate<T>(IUnityContainer container, T entity)
        {
            if (!ValidationRules.ContainsKey(typeof(T)))
                return true;

            var isValid = true;
            foreach (var validationRule in ValidationRules[typeof(T)])
                if (!((Func<IUnityContainer, T, bool>)validationRule)(container, entity))
                {
                    isValid = false;
                }

            return isValid;
        }
    }
}
