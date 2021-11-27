using MasAcademyLab.Domain;
using MasAcademyLab.Service.Models.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MasAcademyLab.Service
{
    public class TokenService : ITokenService
    {
        private readonly IJwtSettings _jwtSettings;

        public TokenService(IJwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string BuildToken(User user)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Issuer, claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryDurationMinutes), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public bool IsTokenValid(string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidAudience = _jwtSettings.Issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
