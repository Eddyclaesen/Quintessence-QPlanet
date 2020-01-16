using System;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QService.DataModel.Prm
{
    public class Project2CrmProject
    {
        [Key, System.ComponentModel.DataAnnotations.Schema.Column(Order = 0)]
        public Guid ProjectId { get; set; }
        
        [Key, System.ComponentModel.DataAnnotations.Schema.Column(Order = 1)]
        public int CrmProjectId { get; set; }
    }
}
