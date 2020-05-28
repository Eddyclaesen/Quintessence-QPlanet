namespace Quintessence.QService.QPlanetService.Implementation
{
    public class AzureAdB2CSettings : IAzureAdB2CSettings
    {
        public string TenantId { get; set; }
        public string ApplicationId { get; set; }
        public string B2cExtensionApplicationId { get; set; }
        public string ClientSecret { get; set; }
    }
}