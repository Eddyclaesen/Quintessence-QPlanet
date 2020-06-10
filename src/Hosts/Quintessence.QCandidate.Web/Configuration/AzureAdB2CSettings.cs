namespace Quintessence.QCandidate.Configuration
{
    public class AzureAdB2CSettings
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