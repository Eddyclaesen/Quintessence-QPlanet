using System;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.WebApi
{
    public class TicketResponse
    {
        public TicketResponse()
        {
            TimeStamp = DateTime.Now;
        }

        public string Ticket { get; set; }
        public string NetServer_url { get; set; }
        public string ContextIdentifier { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
