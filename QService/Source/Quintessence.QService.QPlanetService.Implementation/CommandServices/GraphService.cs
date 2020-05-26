using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace Quintessence.QService.QPlanetService.Implementation.CommandServices
{
    public class AzureAdB2CSettings
    {
        public string TenantId { get; set; }
        public string ApplicationId { get; set; }
        public string B2cExtensionApplicationId { get; set; }
        public string ClientSecret { get; set; }
    }

    public class GraphService
    {
        private readonly AzureAdB2CSettings _settings;

        private readonly string[] _defaultUserAttributes = new[]
        {
            "Id",
            "GivenName",
            "Surname",
            "UserPrincipalName"
        };

        private readonly string[] _customUserAttributes = new[]
        {
            "Language",
            "QPlanet_CandidateId"
        };

        public GraphService(AzureAdB2CSettings settings)
        {
            _settings = settings;
        }

        public List<User> GetAllUsers()
        {
            var graphClient = GetGraphClient();
            var users = graphClient.Users.Request().Select(GetAttributeSelectionString()).GetAsync().Result.ToList();

            return users;
        }

        public User GetUser(Guid id)
        {
            var graphClient = GetGraphClient();
            var user = graphClient.Users[id.ToString()].Request().Select(GetAttributeSelectionString()).GetAsync().Result;

            return user;
        }

        public Guid CreateUser(string firstName, string lastName, string language, string email, Guid qPlanetId)
        {
            IDictionary<string, object> extensionInstance = new Dictionary<string, object>();
            var helper = new B2cCustomAttributeHelper(_settings.B2cExtensionApplicationId);
            extensionInstance.Add(helper.GetCompleteAttributeName("Language"), language);
            extensionInstance.Add(helper.GetCompleteAttributeName("QPlanet_CandidateId"), qPlanetId.ToString());

            var emailFirstPart = email.Split(new[] {'@'}, StringSplitOptions.None).First();
            var userPrincipalName = $"{emailFirstPart}@kenzequintessenceb2cdev.onmicrosoft.com";

            var graphClient = GetGraphClient();
            var user = graphClient.Users
                .Request()
                .AddAsync(new User
                {
                    GivenName = firstName,
                    Surname = lastName,
                    DisplayName = $"{firstName} {lastName}",
                    //UserPrincipalName = userPrincipalName,
                    Mail = email,
                    //MailNickname = Guid.NewGuid().ToString(),
                    AccountEnabled = true,
                    //UserType = "Member",

                    Identities = new List<ObjectIdentity>
                    {
                        new ObjectIdentity()
                        {
                            SignInType = "emailAddress",
                            Issuer = _settings.TenantId,
                            IssuerAssignedId = email
                        }
                    },
                    PasswordProfile = new PasswordProfile()
                    {
                        Password = "Kenze123",//GenerateNewPassword(4, 8, 4)
                        ForceChangePasswordNextSignIn = false,
                        ForceChangePasswordNextSignInWithMfa = false
                    },
                    PasswordPolicies = "DisablePasswordExpiration",
                    AdditionalData = extensionInstance
                }).Result;

            return new Guid(user.Id);
        }

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
            var helper = new B2cCustomAttributeHelper(_settings.B2cExtensionApplicationId);

            return _defaultUserAttributes.Union(_customUserAttributes.Select(a => helper.GetCompleteAttributeName(a))).ToList();
        }

        private string GetAttributeSelectionString()
        {
            return string.Join(",", GetAttributes());
        }

        private static string GenerateNewPassword(int lowercase, int uppercase, int numerics)
        {
            string lowers = "abcdefghijklmnopqrstuvwxyz";
            string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string number = "0123456789";

            Random random = new Random();

            string generated = "!";
            for(int i = 1; i <= lowercase; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    lowers[random.Next(lowers.Length - 1)].ToString()
                );

            for(int i = 1; i <= uppercase; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    uppers[random.Next(uppers.Length - 1)].ToString()
                );

            for(int i = 1; i <= numerics; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    number[random.Next(number.Length - 1)].ToString()
                );

            return generated.Replace("!", string.Empty);
        }

        internal class B2cCustomAttributeHelper
        {
            internal readonly string _b2cExtensionAppClientId;

            internal B2cCustomAttributeHelper(string b2cExtensionAppClientId)
            {
                _b2cExtensionAppClientId = b2cExtensionAppClientId.Replace("-", "");
            }

            internal string GetCompleteAttributeName(string attributeName)
            {
                if(string.IsNullOrWhiteSpace(attributeName))
                {
                    throw new System.ArgumentException("Parameter cannot be null", nameof(attributeName));
                }

                return $"extension_{_b2cExtensionAppClientId}_{attributeName}";
            }
        }
    }
}