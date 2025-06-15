using API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Data.Services
{
    public class TokenService
    {
        private readonly string _secretKey;

        public TokenService()
        {
            _secretKey = Environment.GetEnvironmentVariable("Jwt__Key");
            if (string.IsNullOrEmpty(_secretKey))
                throw new InvalidOperationException("JWT secret key not found in environment variables.");
        }


        public string CreateToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7020",
                audience: "http://localhost:4200",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
