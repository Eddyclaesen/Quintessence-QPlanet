using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Quintessence.QService.DataModel.Dom
{
    [DataContract]
    [Table("{E461EE22-F3E0-44D7-AA1D-B9D08553729A}", Schema = "/ITWeb/QPlanet")]
    public class TrainingChecklist
    {
        [DataMember]
        [Column("UniqueId")]
        public Guid UniqueId { get; set; }

        [DataMember]
        [Column("ID")]
        public int Id { get; set; }
    }
}