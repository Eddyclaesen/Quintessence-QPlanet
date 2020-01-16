using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Sim
{
    public class EditSimulationTranslationModel : BaseEntityModel
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string Name { get; set; }
    }
}