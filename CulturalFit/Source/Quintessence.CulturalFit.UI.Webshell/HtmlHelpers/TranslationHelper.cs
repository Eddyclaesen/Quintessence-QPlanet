using System.Globalization;
using System.Threading;

namespace System.Web.Mvc
{
    public static class TranslationHelper
    {
        public static void GetCulture(int languageId)
        {
            switch (languageId)
            {
                case 1:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("nl");
                    break;

                case 2:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr");
                    break;

                case 3:
                default:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                    break;
            }
        }
    }
}
