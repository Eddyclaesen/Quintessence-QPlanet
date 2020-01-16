using System;

using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TestConsole
{
    class Program : IJobControllerService
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                WriteUsage();
                return 1;
            }

            switch (args[0].ToLower())
            {
                case "associate":
                    Console.WriteLine("Running AssociateReplication ...");
                    new AssociateReplication().Run(new Program());
                    break;
                case "associateemail":
                    Console.WriteLine("Running AssociateEmailReplication ...");
                    new AssociateEmailReplication().Run(new Program());
                    break;
                case "projectid":
                    Console.WriteLine("Running ProjectIdReplication ...");
                    new ProjectIdReplication().Run(new Program());
                    break;
                case "contactid":
                    Console.WriteLine("Running ContactIdReplication ...");
                    new ContactIdReplication().Run(new Program());
                    break;
                case "personid":
                    Console.WriteLine("Running PersonIdReplication ...");
                    new PersonIdReplication().Run(new Program());
                    break;
                case "teamleaderevent":
                    Console.WriteLine("Running TeamLeaderEventReplication ...");
                    new TeamLeaderEventReplication().Run(new Program());
                    break;

                case "duplicate":
                    Console.WriteLine("Running TeamLeaderDuplication ...");
                    new TeamLeaderDuplication.TeamLeaderDuplication().Run(new Program());
                    break;
                case "duplicateuser":
                    Console.WriteLine("Running TeamLeaderUserDuplication ...");
                    new TeamLeaderUserDuplication().Run(new Program());
                    break;
                case "duplicatecontact":
                    Console.WriteLine("Running TeamLeaderContactDuplication ...");
                    new TeamLeaderContactDuplication().Run(new Program());
                    break;
                case "duplicatecompany":
                    Console.WriteLine("Running TeamLeaderCompanyDuplication ...");
                    new TeamLeaderCompanyDuplication().Run(new Program());
                    break;
                case "duplicateproject":
                    Console.WriteLine("Running TeamLeaderProjectDuplication ...");
                    new TeamLeaderProjectDuplication().Run(new Program());
                    break;
                case "duplicatetask":
                    Console.WriteLine("Running TeamLeaderTaskDuplication ...");
                    new TeamLeaderTaskDuplication().Run(new Program());
                    break;
                case "duplicatetimetracking":
                    Console.WriteLine("Running TeamLeaderTimeTrackingDuplication ...");
                    new TeamLeaderTimeTrackingDuplication().Run(new Program());
                    break;
                case "duplicatedeal":
                    Console.WriteLine("Running TeamLeaderDealDuplication ...");
                    new TeamLeaderDealDuplication().Run(new Program());
                    break;
                case "duplicatecall":
                    Console.WriteLine("Running TeamLeaderCallDuplication ...");
                    new TeamLeaderCallDuplication().Run(new Program());
                    break;
                case "duplicatemeeting":
                    Console.WriteLine("Running TeamLeaderMeetingDuplication ...");
                    new TeamLeaderMeetingDuplication().Run(new Program());
                    break;
                case "duplicatecontactcompanyrelation":
                    Console.WriteLine("Running TeamLeaderContactCompanyRelationDuplication ...");
                    new TeamLeaderContactCompanyRelationDuplication().Run(new Program());
                    break;
                case "duplicateplannedtask":
                    Console.WriteLine("Running TeamLeaderPlannedTaskDuplication ...");
                    new TeamLeaderPlannedTaskDuplication().Run(new Program());
                    break;
                case "duplicatecontactprojectrelation":
                    Console.WriteLine("Running TeamLeaderContactProjectRelationDuplication ...");
                    new TeamLeaderContactProjectRelationDuplication().Run(new Program());
                    break;
                case "duplicateproduct":
                    Console.WriteLine("Running TeamLeaderProductDuplication ...");
                    new TeamLeaderProductDuplication().Run(new Program());
                    break;
                case "duplicatemeetingdetail":
                    Console.WriteLine("Running TeamLeaderMeetingDetailDuplication ...");
                    new TeamLeaderMeetingDetailDuplication().Run(new Program());
                    break;
                case "duplicatecompanydetail":
                    Console.WriteLine("Running TeamLeaderCompanyDetailDuplication ...");
                    new TeamLeaderCompanyDetailDuplication().Run(new Program());
                    break;
                case "duplicatetaskdetail":
                    Console.WriteLine("Running TeamLeaderTaskDetailDuplication ...");
                    new TeamLeaderTaskDetailDuplication().Run(new Program());
                    break;
                case "duplicatecalldetail":
                    Console.WriteLine("Running TeamLeaderCallDetailDuplication ...");
                    new TeamLeaderCallDetailDuplication().Run(new Program());
                    break;
                case "duplicatetimetrackingrelatedtask":
                    Console.WriteLine("Running TeamLeaderTimeTrackingRelatedTaskDuplication ...");
                    new TeamLeaderTimeTrackingRelatedTaskDuplication().Run(new Program());
                    break;
            }

            //new TeamLeaderEventReplication().Run(new Program());
            //new AssociateReplication().Run(new Program());          // 1
            //new AssociateEmailReplication().Run(new Program());     // 2
            //new ProjectIdReplication().Run(new Program());          // 3                        
            //new ContactIdReplication().Run(new Program());          // 4
            //new PersonIdReplication().Run(new Program());           // 5

            Console.ReadLine();
            return 0;
        }

        public void WriteInformation(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }

        public void WriteWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
        }

        public void WriteError(string message, Exception exception = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            if (exception != null)
                Console.WriteLine(exception.ToString());
        }

        private static void WriteUsage()
        {
            Console.WriteLine("Usage: TestConsole <replication>");
            Console.WriteLine("Where <replication> can be :");
            Console.WriteLine("Associate");
            Console.WriteLine("AssociateEmail");
            Console.WriteLine("ProjectId");
            Console.WriteLine("ContactId");
            Console.WriteLine("PersonId");
            Console.WriteLine("TeamLeaderEvent");

            Console.WriteLine("DupContact");
        }
    }
}
