using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class EditActivityDetailTrainingLanguageModel : BaseEntityModel
    {
        public string LanguageName { get; set; }
        public int LanguageId { get; set; }
        public int SessionQuantity { get; set; }
    }
}