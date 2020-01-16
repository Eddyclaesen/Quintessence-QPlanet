using System;

namespace Quintessence.CulturalFit.UI.Core.Exceptions
{
    public class MailNotSentException : Exception
    {
        public MailNotSentException(string message)
            : base(message)
        {
        }

        public MailNotSentException(string message, Exception exception)
            : base(message, exception)
        {
        }

    }
}
