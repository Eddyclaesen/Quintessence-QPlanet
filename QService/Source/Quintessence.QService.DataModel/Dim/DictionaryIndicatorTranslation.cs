using System;
using System.Runtime.Serialization;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Dim
{
    public class DictionaryIndicatorTranslation : EntityBase
    {
        private string _text;
        public int LanguageId { get; set; }

        public Guid DictionaryIndicatorId { get; set; }

        public string Text
        {
            get { return _text ?? string.Empty; }
            set { _text = value; }
        }
    }
}
