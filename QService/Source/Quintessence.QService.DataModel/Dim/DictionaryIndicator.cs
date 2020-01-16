using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Dim
{
    public class DictionaryIndicator : EntityBase
    {
        private string _name;
        public Guid DictionaryLevelId { get; set; }

        [Required]
        public string Name
        {
            get { return _name ?? string.Empty; }
            set { _name = value; }
        }

        public int Order { get; set; }
        public bool? IsStandard { get; set; }
        public bool? IsDistinctive { get; set; }
        public int LegacyId { get; set; }
    }
}
