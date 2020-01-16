using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectRoleTranslationModel : BaseEntityModel
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string Text { get; set; }
    }
}