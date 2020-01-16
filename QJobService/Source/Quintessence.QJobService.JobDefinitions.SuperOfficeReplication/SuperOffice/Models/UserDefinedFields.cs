using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.DataAccess;


namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.Models
{
    public class UserDefinedFields
    {
        [JsonProperty(PropertyName = "SuperOffice:1")]
        public string SuperOffice1 { get; set; }

        [JsonProperty(PropertyName = "SuperOffice:1:DisplayText")]
        public string SuperOffice1DisplayText { get; set; }

        [JsonProperty(PropertyName = "SuperOffice:2")]
        public string SuperOffice2 { get; set; }

        [JsonProperty(PropertyName = "SuperOffice:2:DisplayText")]
        public string SuperOffice2DisplayText { get; set; }

        [JsonProperty(PropertyName = "SuperOffice:3")]
        public string SuperOffice3 { get; set; }

        [JsonProperty(PropertyName = "SuperOffice:3:DisplayText")]
        public string SuperOffice3DisplayText { get; set; }

        [JsonProperty(PropertyName = "SuperOffice:4")]
        public string SuperOffice4 { get; set; }

        [JsonProperty(PropertyName = "SuperOffice:4:DisplayText")]
        public string SuperOffice4DisplayText { get; set; }

        [JsonProperty(PropertyName = "SuperOffice:5")]
        public string SuperOffice5 { get; set; }

        [JsonProperty(PropertyName = "SuperOffice:5:DisplayText")]
        public string SuperOffice5DisplayText { get; set; }

        [JsonProperty(PropertyName = "SuperOffice:6")]
        public string SuperOffice6 { get; set; }

        [JsonProperty(PropertyName = "SuperOffice:6:DisplayText")]
        public string SuperOffice6DisplayText { get; set; }

        [JsonProperty(PropertyName = "SuperOffice:7")]
        public string SuperOffice7 { get; set; }

        [JsonProperty(PropertyName = "SuperOffice:7:DisplayText")]
        public string SuperOffice7DisplayText { get; set; }

        [JsonProperty(PropertyName = "SuperOffice:8")]
        public string SuperOffice8 { get; set; }

        [JsonProperty(PropertyName = "SuperOffice:8:DisplayText")]
        public string SuperOffice8DisplayText { get; set; }


        public static string ParseUserDefinedString(string userDefinedValue)
        {
            if (!String.IsNullOrEmpty(userDefinedValue))
            {
                int pFrom = userDefinedValue.IndexOf("\"");
                int pTo = userDefinedValue.LastIndexOf("\"");

                if (pFrom >= 0 && pTo >= 0)
                {
                    pFrom = pFrom + 1;
                    return  userDefinedValue.Substring(pFrom, pTo - pFrom);
                }
            }
            return userDefinedValue;
        }

        public static int? ParseUserDefinedInt(string userDefinedValue)
        {
            int? value = null;
            if (!String.IsNullOrEmpty(userDefinedValue))
            {
                int pFrom = userDefinedValue.IndexOf(":");
                int pTo = userDefinedValue.LastIndexOf("]");

                string valueAsString = userDefinedValue;
                if (pFrom >= 0 && pTo >= 0)
                {
                    pFrom = pFrom + 1;
                    valueAsString = userDefinedValue.Substring(pFrom, pTo - pFrom);
                }
                
                int tmp;
                if (int.TryParse(valueAsString, out tmp))
                    value = tmp;
            }
            return value;
        }

        public static bool ParseUserDefinedBool(string userDefinedValue)
        {
            if (userDefinedValue.Equals("false", StringComparison.InvariantCultureIgnoreCase))
                return false;
            if (userDefinedValue.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                return true;

            return false;
        }

    }
}
