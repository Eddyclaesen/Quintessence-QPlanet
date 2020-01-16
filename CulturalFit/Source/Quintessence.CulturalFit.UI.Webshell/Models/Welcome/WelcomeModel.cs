namespace Quintessence.CulturalFit.UI.Webshell.Models.Welcome
{
    public class WelcomeModel
    {
        public string LanguageGlobalization { get; set; }

        public string RequestCode { get; set; }

        public string ListCode { get; set; }

        public WelcomeType WelcomeTypeText { get; set; }

        public enum WelcomeType
        {
            AsIsParticipant,
            AsIsCustomer,
            ToBe
        }
    }
}