using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServerApp
{
    public static class Config
    {
        private const string VUE_URI = "http://localhost:8080";

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
                // Vue Client
                new Client
                {
                    ClientId = "vue",
                    ClientSecrets =
                    {
                        new Secret("vue_secret")
                    },

                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.Code,

                    //AllowOfflineAccess = true,

                    RedirectUris =
                    {
                        VUE_URI,
                        $"{VUE_URI}/callback.html",
                        $"{VUE_URI}/silent-renew.html"
                    },
                    PostLogoutRedirectUris = 
                    {
                        VUE_URI
                    },
                    AllowedCorsOrigins = 
                    {
                        VUE_URI
                    },

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
