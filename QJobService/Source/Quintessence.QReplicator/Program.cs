using System;
using Quintessence.QJobService.Scheduler;

namespace Quintessence.QReplicator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var jobControllerService = new JobControllerService();
            jobControllerService.StartService();
        }
    }
}
