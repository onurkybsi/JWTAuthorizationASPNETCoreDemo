using JWTAuthorizationASPNETCoreDemo.Models;

namespace JWTAuthorizationASPNETCoreDemo.Services.Repositories
{
    public interface IAppUserRepo
    {
        AppUser GetByHashedPassword(string hashedPassword);
        void Update(AppUser updatedUser);
    }
}