
namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice
{
    public class Constants
    {
        public class SettingKeys
        {
            public const string TicketServiceUri = "SuperOfficeTicketServiceUri";
            public const string TicketServiceApiKey = "SuperOfficeTicketServiceApiKey";
            public const string SuperOfficeBaseUri = "SuperOfficeBaseUri";
            public const string SuperOfficeAppToken = "SuperOfficeAppToken";

            public const string EventBatchSize = "SuperOfficeEventReplicator_EventBatchSize";
            public const string EventMaxProcessCount = "SuperOfficeEventReplicator_EventMaxProcessCount";
        }

        public class EventTypes
        {
            public const string ProjectAdded = "project.created";
            public const string ProjectEdited = "project.changed";
            public const string ProjectDeleted = "project.deleted";

            public const string ContactAdded = "contact.created";
            public const string ContactEdited = "contact.changed";
            public const string ContactDeleted = "contact.deleted";

            public const string PersonAdded = "person.created";
            public const string PersonEdited = "person.changed";
            public const string PersonDeleted = "person.deleted";

            //public const string RelatedContactsUpdated = "related_contacts_updated";

            public const string ActivityAdded = "activity.created";
            public const string ActivityEdited = "activity.changed";
            public const string ActivityDeleted = "activity.deleted";

            //public const string TimeTrackingAdded = "timetracking_created";
            //public const string TimeTrackingEdited = "timetracking_changed";
            //public const string TimeTrackingDeleted = "timetracking_deleted";
        }

        public class ProjectUserRoles
        {
            public const string DecisionMaker = "decision_maker";
            public const string Participant = "participant";
        }
    }
}