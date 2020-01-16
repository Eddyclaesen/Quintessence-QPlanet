using System.ServiceProcess;

namespace Quintessence.QJobService.Scheduler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            JobControllerService service = new JobControllerService();

#if DEBUG
            service.StartService();
            System.Windows.Forms.MessageBox.Show("Click OK to end the service.");
            service.StopService();
#else
            ServiceBase.Run(new ServiceBase[] { service });
#endif
        }
    }
}
