//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class CrmReplicationPerson
    {
        public int Id { get; set; }
        public Nullable<int> ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public Nullable<bool> IsRetired { get; set; }
        public Nullable<int> TeamLeaderId { get; set; }
        public Nullable<System.DateTime> LastSyncedUtc { get; set; }
        public int SyncVersion { get; set; }
        public Nullable<int> SuperOfficeId { get; set; }
    }
}
