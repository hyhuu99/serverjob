using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace JobBoard.Service.Configs
{
    public class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResources.Phone()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("Candidate", "Candidate APIs")
                {
                    // this is needed for introspection when using reference tokens
                    ApiSecrets = { new Secret("1ef1c3c1e9fa48c68519fd6a60b039e4".Sha256()) },
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Address,
                        JwtClaimTypes.PhoneNumber,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Picture,
                        JwtClaimTypes.BirthDate,
                        JwtClaimTypes.Role
                    },
                    
                },
                
                // expanded version if more control is needed
                new ApiResource("Company", "Company APIs")
                {
                    Name = "Company",
                    ApiSecrets =
                    {
                        new Secret("bfe56d6f17d9434d8cae44e108fa5ec0".Sha256())
                    },
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Address,
                        JwtClaimTypes.PhoneNumber,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Picture,
                        JwtClaimTypes.BirthDate,
                        JwtClaimTypes.Role
                    }
                }
            };
        }
    }
}
