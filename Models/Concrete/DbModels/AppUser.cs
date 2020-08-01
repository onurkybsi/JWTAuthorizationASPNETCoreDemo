using JWTAuthorizationASPNETCoreDemo.Models.Abstract.DbModels;

namespace JWTAuthorizationASPNETCoreDemo.Models.Concrete.DbModels
{
    public class AppUser : IAppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}