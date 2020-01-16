using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Dim
{
    public class DictionaryCompetenceTranslation : EntityBase
    {
        private string _text;
        private string _description;
        public int LanguageId { get; set; }
        public Guid DictionaryCompetenceId { get; set; }
        public string Text
        {
            get { return _text ?? string.Empty; }
            set { _text = value; }
        }

        public string Description
        {
            get { return _description ?? string.Empty; }
            set { _description = value; }
        }
    }
}
