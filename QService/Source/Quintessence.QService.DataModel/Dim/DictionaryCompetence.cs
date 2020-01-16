using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Dim
{
    public class DictionaryCompetence : EntityBase
    {
        private string _name;
        public Guid DictionaryClusterId { get; set; }

        [Required]
        public string Name
        {
            get { return _name ?? string.Empty; }
            set { _name = value; }
        }

        public int Order { get; set; }
        public int LegacyId { get; set; }
        public string Description { get; set; }
    }
}
