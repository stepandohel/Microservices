using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace Identity.WebApi.IdentityServer
{
    public static class IdentityConfiguration
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
           new IdentityResource[]
           {
                new IdentityResources.OpenId()
           };
        public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("api1", "My API")
        };
        public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
            {
                new ApiResource("api1", "My API")
                {
                    Scopes = { "api1" }
                }
            };
        public static IEnumerable<Client> Clients =>
    new List<Client>
    {
        new Client
        {
            ClientId = "client",
            AllowAccessTokensViaBrowser = true,
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },
            AllowedScopes = { "api1" }
        }
    };
        public static List<TestUser> GetTestUsers() => new List<TestUser>()
    {
        new TestUser()
        {
            SubjectId = "1",
            Username = "string",
            Password = "string",
            Claims = new List<Claim>()
            {
                 new Claim(JwtClaimTypes.GivenName, "Name"),
                 new Claim(JwtClaimTypes.FamilyName, "Family")
            }
        },
    };
    }
}
