namespace JWTAuthorizationASPNETCoreDemo.Models.Abstract.DbModels
{
    public interface IAppUser
    {
        int Id { get; set; }
        string Email { get; set; }
        string HashedPassword { get; set; }
        string Role { get; set; }
        string Token { get; set; }
    }
}