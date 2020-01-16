namespace Quintessence.QJobService.Interfaces
{
    public interface IJobDefinition
    {
        void Run(IJobControllerService jobControllerService);
    }
}
