using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Restuarant_Management.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restuarant_Management.Service
{
    public class TokenService : IToken
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(string userId, string username, string role)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}




/*
✅ Flow of how this works:

User logs in via TokenController.

Credentials are checked → if valid, TokenService.GenerateToken is called.

A JWT is created with user’s info + signed with your secret key.

Token is returned to client.

Client attaches token in Authorization: Bearer<token> header for future requests.

Middleware validates the token automatically → allows or denies access.
*/