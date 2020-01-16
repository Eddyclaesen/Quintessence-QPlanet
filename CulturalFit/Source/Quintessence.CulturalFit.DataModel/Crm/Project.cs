using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.DataModel.Crm
{
    [DataContract(IsReference = true)]
    public class Project
    {
        [DataMember]
        [Key]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Guid ProjectManagerId { get; set; }

        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public Contact Contact { get; set; }
    }
}   
