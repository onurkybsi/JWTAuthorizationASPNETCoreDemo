using System;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using JWTAuthorizationASPNETCoreDemo.Services.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JWTAuthorizationASPNETCoreDemo.Models;

namespace JWTAuthorizationASPNETCoreDemo.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppSettings _appSettings;
        private readonly IAppUserRepo _repo;
        public AccountService(IOptions<AppSettings> appSettings, IAppUserRepo repo)
        {
            _repo = repo;
            _appSettings = appSettings.Value;
        }

        public AppUser Authenticate(LoginModel login)
        {
            string hashedPassword = Utilities.HashedPassword(login.Password);

            var user = _repo.GetByHashedPassword(hashedPassword);

            if (user is null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            var secretKey = Encoding.ASCII.GetBytes(_appSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Username)
                }),

                Expires = DateTime.UtcNow.AddMinutes(2),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string generatedToken = tokenHandler.WriteToken(token);


            user.Token = generatedToken;
            _repo.Update(user);

            return user;
        }
    }
}