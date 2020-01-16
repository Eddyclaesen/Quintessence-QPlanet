using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Dim
{
    public class DictionaryCluster : EntityBase
    {
        private string _name;
        private string _description;
        public Guid DictionaryId { get; set; }

        [Required]
        public string Name
        {
            get { return _name ?? string.Empty; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description ?? string.Empty; }
            set { _description = value; }
        }

        public int LegacyId { get; set; }
        public int Order { get; set; }
        public string Color { get; set; }
        public string ImageName { get; set; }
    }
}
