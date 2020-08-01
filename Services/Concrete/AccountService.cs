using System;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JWTAuthorizationASPNETCoreDemo.Services.Abstract.Repositories;
using JWTAuthorizationASPNETCoreDemo.Services.Abstract;
using JWTAuthorizationASPNETCoreDemo.Models.Concrete;
using JWTAuthorizationASPNETCoreDemo.Models.Abstract.DbModels;

namespace JWTAuthorizationASPNETCoreDemo.Services.Concrete
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

        public IAppUser Authenticate(LoginModel login)
        {
            var user = _repo.GetByFilter(u => u.Email == login.Email);
            if (user is null) return null;

            string userHash = user.HashedPassword.Split("saltis")[0];
            string userSalt = user.HashedPassword.Split("saltis")[1];
            if (!Utilities.ValidateHash(login.Password, userSalt, userHash)) return null;

            string createdToken = CreateToken(user);

            user.Token = createdToken;
            _repo.Update(user);

            return user;
        }

        public string CreateToken(IAppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var secretKey = Encoding.ASCII.GetBytes(_appSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),

                Expires = DateTime.UtcNow.AddMinutes(0.5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string createdToken = tokenHandler.WriteToken(token);

            return createdToken;
        }
    }
}