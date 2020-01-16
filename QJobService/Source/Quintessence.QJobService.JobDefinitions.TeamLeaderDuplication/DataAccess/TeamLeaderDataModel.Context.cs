﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TeamLeaderEntities : DbContext
    {
        public TeamLeaderEntities()
            : base("name=TeamLeaderEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DuplicationSetting> DuplicationSettings { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Call> Calls { get; set; }
        public virtual DbSet<ContactCompany> ContactCompanies { get; set; }
        public virtual DbSet<PlannedTask> PlannedTasks { get; set; }
        public virtual DbSet<DuplicationErrorLog> DuplicationErrorLogs { get; set; }
        public virtual DbSet<DuplicationJobHistory> DuplicationJobHistories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Deal> Deals { get; set; }
        public virtual DbSet<DealPhase> DealPhases { get; set; }
        public virtual DbSet<DealSource> DealSources { get; set; }
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<ContactProject> ContactProjects { get; set; }
        public virtual DbSet<MeetingContact> MeetingContacts { get; set; }
        public virtual DbSet<MeetingUser> MeetingUsers { get; set; }
        public virtual DbSet<TimeTracking> TimeTrackings { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
    
        public virtual int TruncateDuplicationTables()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("TruncateDuplicationTables");
        }
    }
}
