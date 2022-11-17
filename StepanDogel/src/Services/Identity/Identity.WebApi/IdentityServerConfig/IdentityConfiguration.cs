using IdentityModel;
using IdentityServer4.Models;
using static IdentityServer4.IdentityServerConstants;

namespace Identity.WebApi.IdentityServer
{
    public static class IdentityConfiguration
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new()
                {
                    Name = "roles",
                    DisplayName = "Roles",
                    UserClaims =
                    {
                        JwtClaimTypes.Role
                    }
                },
                new()
                {
                    Name = "id",
                    DisplayName = "Id",
                    UserClaims =
                    {
                        JwtClaimTypes.Id
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new("BasketApi"),
                new("CatalogApi")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new("BasketAPI", new[]
                {
                    JwtClaimTypes.Id,
                    JwtClaimTypes.Role
                })
                {
                    Scopes = new List<string>
                    {
                        "BasketApi"
                    },
                    ApiSecrets = new List<Secret>
                    {
                        new("basketsecret".Sha256())
                    }
                },
                new("CatalogAPI", new[]
                {
                    JwtClaimTypes.Id,
                    JwtClaimTypes.Role
                })
                {
                    Scopes = new List<string>
                    {
                        "CatalogApi"
                    },
                    ApiSecrets = new List<Secret>
                    {
                        new("catalogsecret".Sha256())
                    }
                }
            };

        public static IEnumerable<Client> Clients =>
    new List<Client>
    {
        new Client
        {
            ClientId = "api",
            ClientName = "ClientApi",
            AllowAccessTokensViaBrowser = true,
            ClientSecrets = new[]
            {
                new Secret("clientsecret".Sha512())
            },
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            AllowedScopes =
            {
                StandardScopes.OpenId,
                "BasketApi",
                "OrdersApi"
            }
        }
    };
    }
}
