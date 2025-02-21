using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bank.Domain.Models;
using Bank.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Bank.Applications.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<bool> RegisterClient(string name, string email, string password)
        {
            var existingClient = await _authRepository.GetClientByEmail(email);
            if (existingClient != null)
                return false;

            var salt = GenerateSalt();
            var passwordHash = HashPassword(password, salt);

            var client = new Client
            {
                Name = name,
                Email = email,
                PasswordHash = passwordHash,
                Salt = salt
            };

            await _authRepository.AddClient(client);
            return true;
        }

        public async Task<string?> AuthenticateClient(string email, string password)
        {
            var client = await _authRepository.GetClientByEmail(email);
            if (client == null) return null;

            // Vérifie le mot de passe
            var hashedPassword = HashPassword(password, client.Salt);
            if (hashedPassword != client.PasswordHash)
                return null;

            // Génère un Token JWT
            return GenerateJwtToken(client);
        }

        // Méthodes de hash du mot de passe
        private string GenerateSalt()
        {
            var saltBytes = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            using var hmac = new HMACSHA256(Convert.FromBase64String(salt));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        // Génération du Token JWT
        private string GenerateJwtToken(Client client)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, client.Email),
            new Claim("name", client.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
