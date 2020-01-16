using System;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectRole2DictionaryLevel
    {
        [Key, System.ComponentModel.DataAnnotations.Schema.ForeignKey("ProjectRole")]
        public Guid ProjectRoleId { get; set; }

        [Key, System.ComponentModel.DataAnnotations.Schema.ForeignKey("DictionaryLevel")]
        public Guid DictionaryLevelId { get; set; }
    }
}
