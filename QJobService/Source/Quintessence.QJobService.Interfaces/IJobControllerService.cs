using System;

namespace Quintessence.QJobService.Interfaces
{
    public interface IJobControllerService
    {
        void WriteInformation(string message);
        void WriteWarning(string message);
        void WriteError(string message, Exception exception = null);
    }
}
