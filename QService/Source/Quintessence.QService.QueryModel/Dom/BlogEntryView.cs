using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Dom
{
    [DataContract]
    [Table("{A6F82531-21D2-4551-8E95-9D742C6D676B}", Schema = "/ITWeb")]
    public class BlogEntryView : SharePointEntityView
    {
        [DataMember]
        [Column("Body")]
        public string Body { get; set; }

        [DataMember]
        [Column("Title")]
        public string Title { get; set; }

        [DataMember]
        [Column("Created")]
        public DateTime Created { get; set; }

        [DataMember]
        [Column("Modified")]
        public DateTime? Modified { get; set; }

        [DataMember]
        [Column("IsSticky")]
        public bool? IsSticky { get; set; }

        [DataMember]
        [Column("Expires")]
        public DateTime? Expires { get; set; }
    }
}