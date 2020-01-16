using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Dom
{
    [DataContract]
    public class SharePointEntityView
    {
        [DataMember]
        [Column("UniqueId")]
        public Guid UniqueId { get; set; }

        [DataMember]
        [Column("ID")]
        public int Id { get; set; }
    }
}