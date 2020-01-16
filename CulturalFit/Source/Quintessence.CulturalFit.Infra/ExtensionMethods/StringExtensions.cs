namespace Quintessence.CulturalFit.Infra.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string CapitalizeFirstLetters(this string myString)
        {
            if (string.IsNullOrEmpty(myString))
                return string.Empty;
            var strings = myString.Split(' ');
            foreach (var s in strings)
            {
                var capitalized = char.ToUpper(s[0]) + s.Substring(1);
                myString = myString.Replace(s, capitalized);
            }
            return myString;
        }
    }
}
