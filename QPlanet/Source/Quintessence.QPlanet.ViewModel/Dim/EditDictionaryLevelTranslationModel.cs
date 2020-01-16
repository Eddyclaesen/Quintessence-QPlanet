using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class EditDictionaryLevelTranslationModel : BaseEntityModel
    {
        public string Text { get; set; }
        public string LanguageName { get; set; }
        public int LanguageId { get; set; }
    }
}