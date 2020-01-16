using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.Infrastructure.Validation
{
    [DataContract]
    public class ValidationContainer
    {
        private FaultDetail _faultDetail;

        [DataMember]
        public FaultDetail FaultDetail
        {
            get { return _faultDetail ?? (_faultDetail = new FaultDetail()); }
            set { _faultDetail = value; }
        }

        public void RegisterException(Exception exception)
        {
            FaultDetail.RegisterException(exception);
        }

        public void Validate()
        {
            if (!FaultDetail.IsValid)
                throw new FaultException<ValidationContainer>(this, Reason);
        }

        protected string Reason
        {
            get { return FaultDetail.Reason; }
        }

        public void RegisterVersionMismatchEntry(object updateProjectRequest, IEntity project)
        {
            FaultDetail.RegisterVersionMismatchEntry(updateProjectRequest, project);
        }

        public void RegisterAuthenticationFaultEntry(string message)
        {
            FaultDetail.RegisterAuthenticationFaultEntry(message);
        }

        public bool ValidateObject(IEntity entity)
        {
            var results = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(entity);
            var isValid = Validator.TryValidateObject(entity, validationContext, results, true);

            //TODO: remove line because Validate is being called by TryValidateObject in line above.
            //results.AddRange(entity.Validate(validationContext));

            results.ForEach(result => RegisterEntityValidationFaultEntry(result.ErrorMessage, result.MemberNames.ToArray()));

            return isValid;
        }

        public void RegisterEntityValidationFaultEntry(string errorMessage, params string[] memberNames)
        {
            FaultDetail.RegisterEntityValidationFaultEntry(errorMessage, memberNames);
        }

        public void RegisterValidationFaultEntry(string code, string message)
        {
            FaultDetail.RegisterValidationFaultEntry(code, message);
        }
    }
}
