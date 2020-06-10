namespace Quintessence.QCandidate.Models
{
    public class Settings
    {
        public string PdfStorageLocation { get; set; }
        public AzureAdB2C AzureAdB2C { get; set; }
    }

    public class AzureAdB2C
    {
        public string Instance { get; set; }
        public string Tenant { get; set; }
        public string ClientId { get; set; }
        public string Domain { get; set; }
        public string SignUpSignInPolicyId { get; set; }
        public string ResetPasswordPolicyId { get; set; }
        public string EditProfilePolicyId { get; set; }
    }
}