using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PhoneBookBackend.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;



namespace PhoneBookBackend.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public string GenerateToken(UserLogin user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:JWTkey"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
            };

            var tokenOptions = new JwtSecurityToken(
               issuer: _configuration["JWT:Issuer"],
               audience: _configuration["JWT:Audience"],
               claims: claims,
               expires: DateTime.Now.AddHours(3),
               signingCredentials: signingCredentials
               );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }

        public bool VerifiedPassword(UserLogin user)
        {
            var account = _userService.GetUserByEmail(user.Email);
            return BC.Verify(user.Password, account.Password);
        }
    }
}
