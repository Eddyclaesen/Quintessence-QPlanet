using System;
using System.Runtime.Serialization;
using Quintessence.Infrastructure.Model.DataModel;
using Quintessence.QService.DataModel.Inf;

namespace Quintessence.QService.DataModel.Dim
{
    public class DictionaryLevelTranslation : EntityBase
    {
        private string _text;
        public int LanguageId { get; set; }

        public Guid DictionaryLevelId { get; set; }

        public string Text
        {
            get { return _text ?? string.Empty; }
            set { _text = value; }
        }
    }
}