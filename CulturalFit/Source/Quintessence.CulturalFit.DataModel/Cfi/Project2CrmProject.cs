using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Quintessence.CulturalFit.DataModel.Cfi
{
    [DataContract]
    public class Project2CrmProject
    {
        [DataMember]
        public int CrmProjectId { get; set; }

        [DataMember]
        [Key]
        public Guid ProjectId { get; set; }
    }
}
