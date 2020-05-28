namespace Quintessence.QService.QPlanetService.Implementation
{
    public interface IAzureAdB2CSettings
    {
        string TenantId { get; set; }
        string ApplicationId { get; set; }
        string B2cExtensionApplicationId { get; set; }
        string ClientSecret { get; set; }
    }
}