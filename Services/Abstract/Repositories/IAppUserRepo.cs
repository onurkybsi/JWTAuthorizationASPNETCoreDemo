using System.Collections.Generic;
using JWTAuthorizationASPNETCoreDemo.Models.Abstract.DbModels;

namespace JWTAuthorizationASPNETCoreDemo.Services.Abstract.Repositories
{
    public interface IAppUserRepo
    {
        IEnumerable<IAppUser> GetAllUsers();
        IAppUser GetByUserEmail(string email);
        void Add(IAppUser user);
        void Update(IAppUser user);
    }
}