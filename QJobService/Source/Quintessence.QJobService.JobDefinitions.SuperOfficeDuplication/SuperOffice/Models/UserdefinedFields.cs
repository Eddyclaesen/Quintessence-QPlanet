using System;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models
{
    public class UserDefinedFields
    {

        public static string ParseUserDefinedString(string userDefinedValue)
        {
            if (!String.IsNullOrEmpty(userDefinedValue))
            {
                int pFrom = userDefinedValue.IndexOf("\"");
                int pTo = userDefinedValue.LastIndexOf("\"");

                if (pFrom >= 0 && pTo >= 0)
                {
                    pFrom = pFrom + 1;
                    return userDefinedValue.Substring(pFrom, pTo - pFrom);
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
