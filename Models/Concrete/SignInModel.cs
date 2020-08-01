using System.ComponentModel.DataAnnotations;

namespace JWTAuthorizationASPNETCoreDemo.Models.Concrete
{
    public class SignInModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}