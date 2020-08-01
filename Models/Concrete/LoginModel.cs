using System.ComponentModel.DataAnnotations;

namespace JWTAuthorizationASPNETCoreDemo.Models.Concrete
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}