using System;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice
{
    public class Constants
    {
        public class SettingKeys
        {
            public const string TicketServiceUri = "SuperOfficeTicketServiceUri";
            public const string TicketServiceApiKey = "SuperOfficeTicketServiceApiKey";
            public const string SuperOfficeCustomerStateUri = "SuperOfficeCustomerStateUri";
            public const string SuperOfficeAppToken = "SuperOfficeAppToken";

            public const string EventBatchSize = "SuperOfficeEventReplicator_EventBatchSize";
            public const string EventMaxProcessCount = "SuperOfficeEventReplicator_EventMaxProcessCount";
            public const string DuplicatorBatchSize = "SuperOffice_DuplicatorBatchSize";
        }
    }
}
