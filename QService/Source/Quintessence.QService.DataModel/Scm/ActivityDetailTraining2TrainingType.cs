using System;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QService.DataModel.Scm
{
    public class ActivityDetailTraining2TrainingType
    {
        [Key]
        public Guid ActivityDetailTrainingId { get; set; }

        [Key]
        public Guid TrainingTypeId { get; set; }
    }
}
