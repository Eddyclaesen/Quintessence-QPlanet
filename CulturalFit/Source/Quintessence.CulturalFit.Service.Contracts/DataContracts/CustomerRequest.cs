using System;
using Quintessence.CulturalFit.Service.Contracts.DataContracts.Base;

namespace Quintessence.CulturalFit.Service.Contracts.DataContracts
{
    public class CustomerRequest: BaseRequest
    {
        public Guid? ContactPersonId { get; set; }
        public string ContactPersonFirstName { get; set; }
        public string ContactPersonLastName { get; set; }
        public string ContactPersonEmail { get; set; }
        public int ContactPersonGender { get; set; }
        public int LanguageId { get; set; }
    }
}
