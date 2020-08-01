using JWTAuthorizationASPNETCoreDemo.Models.Abstract.DbModels;
using JWTAuthorizationASPNETCoreDemo.Models.Concrete;

namespace JWTAuthorizationASPNETCoreDemo.Services.Abstract
{
    public interface IAccountService
    {
        IAppUser Authenticate(LoginModel user);
        string CreateToken(IAppUser user);
    }
}