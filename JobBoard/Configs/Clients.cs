using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace JobBoard.Service.Configs
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "7af65a43-0eb4-4734-9ab8-29901e795399",
                    ClientSecrets =
                    {
                        new Secret
                        (
                            "secret".Sha256()
                        )
                    },

                    UpdateAccessTokenClaimsOnRefresh = true,
                    //This feature refresh token
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 3600*90,
                    //IdentityTokenLifetime = 3600*90,
                    AlwaysSendClientClaims = true,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = {
                        "Candidate",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Address,
                    },
                    AccessTokenType = AccessTokenType.Jwt
                },
                new Client
                {
                    ClientId = "272g9416-36ab-4261-bd84-3c849b51b9c4",
                    ClientSecrets =
                    {
                        new Secret
                        (
                            "secret".Sha256()
                        )
                    },
                    AccessTokenLifetime = 3600*90,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = {
                        "Company",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Address,
                    }
                }

            };
        }
    }
}
