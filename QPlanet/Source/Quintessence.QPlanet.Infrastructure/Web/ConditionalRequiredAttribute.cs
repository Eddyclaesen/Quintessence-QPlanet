using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;

namespace Quintessence.QPlanet.Infrastructure.Web
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ConditionalRequiredAttribute : RequiredAttribute, IClientValidatable
    {
        private readonly PropertyInfo _conditionalPropertyInfo;

        public ConditionalRequiredAttribute(string conditionalPropertyName, Type o)
        {
            _conditionalPropertyInfo = o.GetProperty(conditionalPropertyName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((bool)_conditionalPropertyInfo.GetValue(validationContext.ObjectInstance))
                return base.IsValid(value, validationContext);
            return base.IsValid(true, validationContext);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new[]
        {
            new ModelClientValidationRequiredIfRule(string.Format(ErrorMessageString, 
                metadata.GetDisplayName()), _conditionalPropertyInfo.Name)
        };
        }

        public class ModelClientValidationRequiredIfRule : ModelClientValidationRule
        {
            public ModelClientValidationRequiredIfRule(string errorMessage,
                                                       string otherProperty)
            {
                ErrorMessage = errorMessage;
                ValidationType = "conditionalrequired";
                ValidationParameters.Add("other", otherProperty);
                ValidationParameters.Add("value", "true");
            }
        }
    }
}
