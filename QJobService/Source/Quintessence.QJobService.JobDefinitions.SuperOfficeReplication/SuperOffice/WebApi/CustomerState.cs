using System;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.WebApi
{
    public class CustomerState
    {
        private static TimeSpan MinValidityDuration = TimeSpan.FromMinutes(2);
        private static string VersionSuffix = "/v1/";


        public string ContextIdentifier { get; set; }

        public string Endpoint { get; set; }

        public DateTime ValidUntil { get; set; }

        public string State { get; set; }

        public bool IsRunning { get; set; }

        public string Api { get; set; }

        public bool IsValid()
        {
            return ValidUntil.Subtract(DateTime.UtcNow) > MinValidityDuration;
        }


        public string SuperOfficeBaseUri 
        { 
            get
            {
                return Api + VersionSuffix;
            }
            
        }
    }
}