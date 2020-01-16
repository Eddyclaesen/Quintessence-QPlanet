using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class EditDictionaryCompetenceTranslationModel : BaseEntityModel
    {
        public string Text { get; set; }
        public string Description { get; set; }
        public string LanguageName { get; set; }
        public int LanguageId { get; set; }
    }
}