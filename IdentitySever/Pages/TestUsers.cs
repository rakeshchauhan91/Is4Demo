// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using IdentityModel;
using System.Security.Claims;
using System.Text.Json;
using Duende.IdentityServer;
using Duende.IdentityServer.Test;

namespace IdentitySever;

public class TestUsers
{
    public static List<TestUser> Users
    {
        get
        {
            var address = new
            {
                street_address = "One Hacker Way",
                locality = "Chandigarh",
                postal_code = 160014,
                country = "India"
            };
                
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "rakesh",
                    Password = "test@123",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Rakesh Chauhan"),
                        new Claim(JwtClaimTypes.GivenName, "Rakesh"),
                        new Claim(JwtClaimTypes.FamilyName, "Chauhan"),
                        new Claim(JwtClaimTypes.Email, "rakesh376@gmail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://rakesh376.com"),
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                    }
                } 
            };
        }
    }
}