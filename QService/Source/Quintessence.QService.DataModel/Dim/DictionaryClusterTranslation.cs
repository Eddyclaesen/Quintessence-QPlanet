using System;
using System.Runtime.Serialization;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Dim
{
    public class DictionaryClusterTranslation : EntityBase
    {
        private string _text;
        private string _description;
        public int LanguageId { get; set; }
        public Guid DictionaryClusterId { get; set; }
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
