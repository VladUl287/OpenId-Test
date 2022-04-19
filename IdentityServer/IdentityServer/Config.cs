using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
          new List<IdentityResource>
          {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
          };


        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("api1", "My API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // machine to machine client
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                
                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,

                    //AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = 330,
                    IdentityTokenLifetime = 300,
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = new[]
                    {
                        "http://localhost:8080",
                        "http://localhost:8080/callback.html",
                        "http://localhost:8080/silent-renew.html",
                    },
                    PostLogoutRedirectUris = { "http://localhost:8080/" },
                    AllowedCorsOrigins =     { "http://localhost:8080" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                }
            };
    }
}