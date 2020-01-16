using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Dim
{
    public class DictionaryLevel : EntityBase
    {
        private string _name;
        public Guid DictionaryCompetenceId { get; set; }
        public string Name
        {
            get { return _name ?? string.Empty; }
            set { _name = value; }
        }

        public int Level { get; set; }
        public int LegacyId { get; set; }
    }
}
