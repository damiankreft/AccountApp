using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AccountApp.Infrastructure.Dto;
using AccountApp.Infrastructure.Extensions;
using AccountApp.Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;

namespace AccountApp.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _settings;

        public JwtHandler(JwtSettings settings)
        {
            _settings = settings;
        }
        public JwtDto CreateToken(string email)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString(), ClaimValueTypes.Integer64)
            };

            var expires = now.AddMinutes(_settings.ExpiryMinutes);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key)), SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken
            (
                issuer: _settings.Issuer,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_settings.ExpiryMinutes),
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                Token = token,
                Expiry = expires.ToTimestamp()
            };
        }
    }
}