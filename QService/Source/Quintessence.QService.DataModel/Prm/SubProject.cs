using System;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QService.DataModel.Prm
{
    public class SubProject
    {
        [Key]
        public Guid ProjectId { get; set; }
        [Key]
        public Guid SubProjectId { get; set; }
        public Guid? ProjectCandidateId { get; set; }
    }
}