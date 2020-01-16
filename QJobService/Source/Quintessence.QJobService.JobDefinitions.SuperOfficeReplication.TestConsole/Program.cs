using System;

using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.TestConsole
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
                case "superofficeevent":
                    Console.WriteLine("Running SuperOfficeEventReplication ...");
                    new SuperOfficeEventReplication().Run(new Program());
                    break;

                case "duplicate":
                    Console.WriteLine("Running SuperOfficeDuplication ...");
                    new SuperOfficeDuplication.SuperOfficeDuplication().Run(new Program());
                    break;
                case "duplicateuser":
                    Console.WriteLine("Running SuperOfficeUserDuplication ...");
                    new SuperOfficeDuplication.SuperOfficeUserDuplication().Run(new Program());
                    break;
                case "duplicateperson":
                    Console.WriteLine("Running SuperOfficePersonDuplication ...");
                    new SuperOfficeDuplication.SuperOfficePersonDuplication().Run(new Program());
                    break;
                case "duplicatecontact":
                    Console.WriteLine("Running SuperOfficeContactDuplication ...");
                    new SuperOfficeDuplication.SuperOfficeContactDuplication().Run(new Program());
                    break;
                case "duplicateproject":
                    Console.WriteLine("Running SuperOfficeProjectDuplication ...");
                    new SuperOfficeDuplication.SuperOfficeProjectDuplication().Run(new Program());
                    break;
                case "duplicatesale":
                    Console.WriteLine("Running SuperOfficeSaleDuplication ...");
                    new SuperOfficeDuplication.SuperOfficeSaleDuplication().Run(new Program());
                    break;
                case "duplicateappointment":
                    Console.WriteLine("Running SuperOfficeAppointmentDuplication ...");
                    new SuperOfficeDuplication.SuperOfficeAppointmentDuplication().Run(new Program());
                    break;
                case "duplicateappointmentdetail":
                    Console.WriteLine("Running SuperOfficeAppointmentDetailDuplication ...");
                    new SuperOfficeDuplication.SuperOfficeAppointmentDetailDuplication().Run(new Program());
                    break;
                case "duplicateprojectmember":
                    Console.WriteLine("Running SuperOfficeProjectMemberDuplication ...");
                    new SuperOfficeDuplication.SuperOfficeProjectMemberDuplication().Run(new Program());
                    break;
            }

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
            Console.WriteLine("SuperOfficeEvent");

            Console.WriteLine("DupContact");
        }
    }
}
