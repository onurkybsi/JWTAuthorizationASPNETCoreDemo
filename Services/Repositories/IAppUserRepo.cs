using JWTAuthorizationASPNETCoreDemo.Models;

namespace JWTAuthorizationASPNETCoreDemo.Services.Repositories
{
    public interface IAppUserRepo
    {
        AppUser GetByUserName(string username);
        void Update(AppUser updatedUser);
    }
}