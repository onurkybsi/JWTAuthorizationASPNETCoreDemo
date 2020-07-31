using JWTAuthorizationASPNETCoreDemo.Models;

namespace JWTAuthorizationASPNETCoreDemo.Services
{
    public interface IAccountService
    {
        AppUser Authenticate(LoginModel user);
    }
}