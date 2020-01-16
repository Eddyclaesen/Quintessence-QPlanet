//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Company
    {
        public int Id { get; set; }
        public int TeamLeaderId { get; set; }
        public string Name { get; set; }
        public string TaxCode { get; set; }
        public string BusinessType { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Iban { get; set; }
        public string Bic { get; set; }
        public string LanguageCode { get; set; }
        public string Afdeling { get; set; }
        public Nullable<int> AccountManagerId { get; set; }
        public string AccountManagerTwo { get; set; }
        public string Assistent { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<System.DateTime> DateEdited { get; set; }
        public string Status { get; set; }
        public string Business { get; set; }
        public string FidelisatieNorm { get; set; }
        public Nullable<bool> FocusList { get; set; }
        public string LinkedInProfile { get; set; }
        public string Sector { get; set; }
        public System.DateTime LastSyncedUtc { get; set; }
    }
}
