//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Project
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string ProjectAssociateFullName { get; set; }
        public Nullable<int> ProjectAssociatePersonId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> RegisteredDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string BookYear { get; set; }
        public Nullable<System.DateTime> NextMileStone { get; set; }
        public Nullable<bool> Completed { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime LastSyncedUtc { get; set; }
    }
}
