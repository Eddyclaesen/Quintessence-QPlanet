//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class CrmReplicationTeamLeaderEvent
    {
        public int Id { get; set; }
        public string EventType { get; set; }
        public string ObjectType { get; set; }
        public string ObjectId { get; set; }
        public string Source { get; set; }
        public System.DateTime ReceivedUtc { get; set; }
        public int ProcessCount { get; set; }
    }
}
