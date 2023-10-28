using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentitySever;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),

        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
                new ApiScope(name: "api1", displayName: "MyAPI")  };

    public static IEnumerable<Client> Clients =>
        new Client[]
            {
                new Client
                 {
                    // m2m client
                    ClientId = "m2mClient",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                // interactive ASP.NET Core Web App
                new Client
                {
                    ClientId = "web",
                    ClientSecrets = { new Secret("websecret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                     AllowOfflineAccess = true,

                    // where to redirect to after login
                    RedirectUris = { "https://localhost:7047/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:7047/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                         "api1"
                    }
                }
            };
}