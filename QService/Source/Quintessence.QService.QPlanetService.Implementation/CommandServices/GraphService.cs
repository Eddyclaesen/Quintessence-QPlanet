using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace Quintessence.QService.QPlanetService.Implementation.CommandServices
{
    public interface IGraphService
    {
        Guid CreateUser(string firstName, string lastName, string language, string email, Guid qPlanetId, string password);
        void UpdateUser(Guid id, string firstName, string lastName, string language, string email);
    }

    public class GraphService : IGraphService
    {
        private readonly IAzureAdB2CSettings _settings;

        private readonly string[] _defaultUserAttributes = new[]
        {
            "Id",
            "GivenName",
            "Surname",
            "UserPrincipalName",
            "Identities",
            "PasswordProfile"
        };

        private readonly string[] _customUserAttributes = new[]
        {
            "Language",
            "QPlanet_CandidateId"
        };

        public GraphService(IAzureAdB2CSettings settings)
        {
            _settings = settings;
        }

        public Guid CreateUser(string firstName, string lastName, string language, string email, Guid qPlanetId, string password)
        {
            IDictionary<string, object> extensionInstance = new Dictionary<string, object>();
            extensionInstance.Add(GetCompleteAttributeName("Language"), language);
            extensionInstance.Add(GetCompleteAttributeName("QPlanet_CandidateId"), qPlanetId.ToString());

            var graphClient = GetGraphClient();
            var user = graphClient.Users
                .Request()
                .AddAsync(new User
                {
                    GivenName = firstName,
                    Surname = lastName,
                    DisplayName = GetDisplayName(firstName, lastName),
                    Mail = email,
                    AccountEnabled = true,

                    Identities = new List<ObjectIdentity>
                    {
                        new ObjectIdentity()
                        {
                            SignInType = "emailAddress",
                            Issuer = _settings.TenantId,
                            IssuerAssignedId = email
                        }
                    },
                    PasswordProfile = GetPasswordProfile(password),
                    PasswordPolicies = "DisablePasswordExpiration",
                    AdditionalData = extensionInstance
                }).Result;

            return new Guid(user.Id);
        }

        public void UpdateUser(Guid id, string firstName, string lastName, string language, string email)
        {
            IDictionary<string, object> extensionInstance = new Dictionary<string, object>();
            extensionInstance.Add(GetCompleteAttributeName("Language"), language);

            var updatedUser = new User
            {
                GivenName = firstName,
                Surname = lastName,
                DisplayName = GetDisplayName(firstName, lastName),
                Mail = email,
                //sign-in e-mail address will not be changed, since the candidate
                //received this as part of his/her login details
                AdditionalData = extensionInstance
            };

            var graphClient = GetGraphClient();
            graphClient.Users[id.ToString()]
                .Request()
                .UpdateAsync(updatedUser)
                .Wait();
        }

        private PasswordProfile GetPasswordProfile(string password)
        {
            return new PasswordProfile()
            {
                Password = password,
                ForceChangePasswordNextSignIn = false,
                ForceChangePasswordNextSignInWithMfa = false
            };
        }

        private string GetDisplayName(string firstName, string lastName) => $"{firstName} {lastName}";

        private GraphServiceClient GetGraphClient()
        {
            var confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(_settings.ApplicationId)
                .WithTenantId(_settings.TenantId)
                .WithClientSecret(_settings.ClientSecret)
                .Build();
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var authProvider = new DelegateAuthenticationProvider(async (requestMessage) =>
            {
                // Retrieve an access token for Microsoft Graph (gets a fresh token if needed).
                var authResult = await confidentialClientApplication
                    .AcquireTokenForClient(scopes)
                    .ExecuteAsync();

                // Add the access token in the Authorization header of the API request.
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
            });
            var graphClient = new GraphServiceClient(authProvider);

            return graphClient;
        }

        private List<string> GetAttributes()
        {
            return _defaultUserAttributes.Union(_customUserAttributes.Select(GetCompleteAttributeName)).ToList();
        }

        private string GetAttributeSelectionString()
        {
            return string.Join(",", GetAttributes());
        }
        
        private string GetCompleteAttributeName(string attributeName)
        {
            if(string.IsNullOrWhiteSpace(attributeName))
            {
                throw new ArgumentException("Parameter cannot be null", nameof(attributeName));
            }

            var b2cExtensionAppClientId = _settings.B2cExtensionApplicationId.Replace("-", "");

            return $"extension_{b2cExtensionAppClientId}_{attributeName}";
        }
    }
}