
namespace Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader
{
    public class Constants
    {
        public class SettingKeys
        {
            public const string ApiGroup = "TeamLeaderAPI_Group";
            public const string ApiKey = "TeamLeaderAPI_Key";
            public const string ApiBaseUrl = "TeamLeaderAPI_BaseUrl";
            public const string EventBatchSize = "TeamLeaderEventReplicator_EventBatchSize";
            public const string EventMaxProcessCount = "TeamLeaderEventReplicator_EventMaxProcessCount";
            public const string DuplicatorBatchSize = "TeamLeaderDuplicator_DuplicatorBatchSize";
        }

        public class CustomFieldObjectType
        {
            public const string Bool = "boolean";
            public const string String = "string";
            public const string Number = "number";
            public const string Enum = "enum";
            public const string Set = "set";
        }

        public class CustomFieldTargetObject
        {
            public const string Company = "company";
            public const string Contact = "contact";            
            public const string Project = "project";
            public const string Task = "todo";
            public const string Meeting = "meeting";
            public const string Call = "callback";
            public const string Deal = "sale";
        }

        public class CustomFieldNames
        {
            public class Company
            {
                public const string Accountmanager = "Accountmanager 2";
                public const string Afdeling = "Afdeling";
                public const string Assistent = "Assistent";
                public const string Business = "Business";
                public const string Fidelisatienorm = "Fidelisatienorm";
                public const string FocusList = "Focuslijst";
                public const string LinkedInProfile = "LinkedIn profiel";
                public const string LegacyId = "LegacyId";
                public const string Team = "Team";
            }

            public class Contact
            {
                public const string Events = "Events";
                public const string FormerEmployee = "Former employee";
                public const string Functietitel = "Functietitel";
                public const string Inside = "Inside";
                public const string LegacyId = "LegacyId";
                public const string Mailing = "Mailing";
                public const string Opleiding = "Opleiding";
                public const string Positie = "Positie";
            }

            public class Project
            {
                public const string Boekjaar = "Boekjaar";
                public const string LegacyId = "LegacyId";
                public const string Status = "Status";
            }

            public class TeamLeaderTask
            {
                public const string FirstName = "1 - Voornaam";
                public const string LastName = "2 - Achternaam";
                public const string Gender = "3 - Geslacht";
                public const string Language = "4 - Taal";
                public const string Email = "5 - Email";
                public const string Code = "6 - Code";
                public const string Reservatie = "7 - Reservatie";
                public const string Lokatie = "8 - Locatie";
                public const string Intern = "Intern";
                public const string Klant = "Klant";
                public const string Overige = "Overige";
                public const string Sales = "Sales";
            }

            public class Meeting
            {
                public const string Afwezig = "Afwezig";
                public const string Bedrijf = "Bedrijf";
                public const string Klant = "Klant";
                public const string Intern = "Intern";
                public const string Sales = "Sales";
            }

            public class Call
            {
                public const string Intern = "Intern";
                public const string Klant = "Klant";
                public const string Sales = "Sales";
            }

            public class Deal
            {
                public const string Probability = "Kans op succes in percentage";
            }
        }


        public class EventTypes
        {
            public const string ProjectAdded = "project_added";
            public const string ProjectEdited = "project_edited";
            public const string ProjectDeleted = "project_deleted";

            public const string ContactAdded = "contact_added";
            public const string ContactEdited = "contact_edited";
            public const string ContactDeleted = "contact_deleted";

            public const string CompanyAdded = "company_added";
            public const string CompanyEdited = "company_edited";
            public const string CompanyDeleted = "company_deleted";

            public const string RelatedContactsUpdated = "related_contacts_updated";

            public const string TaskAdded = "task_added";
            public const string TaskEdited = "task_edited";
            public const string TaskDeleted = "task_deleted";

            public const string TimeTrackingAdded = "timetracking_added";
            public const string TimeTrackingEdited = "timetracking_edited";
            public const string TimeTrackingDeleted = "timetracking_deleted";
        }


        public class ObjectTypes
        {
            public const string Company = "company";
            public const string Project = "project";
        }

        public class ProjectUserRoles
        {
            public const string DecisionMaker = "decision_maker";
            public const string Participant = "participant";
        }

        public class ContactOrCompany
        {
            public const string Company = "company";
            public const string Contact = "contact";
        }
    }
}
