using System.Collections.Generic;
using JWTAuthorizationASPNETCoreDemo.Models;

namespace JWTAuthorizationASPNETCoreDemo.Services.Repositories
{
    public interface IAppUserRepo
    {
        IEnumerable<AppUser> GetAllUsers();
        AppUser GetByUserName(string username);
        void Update(AppUser updatedUser);
    }
}