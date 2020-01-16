using System;
using System.Configuration;
using Quintessence.QPlanet.Infrastructure.Logging;

namespace Quintessence.QPlanet.Webshell.Infrastructure
{
    public static class Configuration
    {
        public static bool GetImportantMessage(out string message)
        {
            try
            {
                return !string.IsNullOrWhiteSpace(message = ConfigurationManager.AppSettings["ImportantMessage"]);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                message = string.Empty;
                return false;
            }
        }
    }
}