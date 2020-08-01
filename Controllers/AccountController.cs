using JWTAuthorizationASPNETCoreDemo.Models.Concrete;
using JWTAuthorizationASPNETCoreDemo.Models.Concrete.DbModels;
using JWTAuthorizationASPNETCoreDemo.Services.Abstract;
using JWTAuthorizationASPNETCoreDemo.Services.Abstract.Repositories;
using JWTAuthorizationASPNETCoreDemo.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthorizationASPNETCoreDemo.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAppUserRepo _repo;

        public AccountController(IAccountService accountService, IAppUserRepo repo)
        {
            _accountService = accountService;
            _repo = repo;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var result = _accountService.Authenticate(login);

            if (result is null)
                return BadRequest();
            else
                return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_repo.GetAllUsers());
        }

        [HttpPost]
        public IActionResult SignIn([FromBody] SignInModel newUser)
        {
            string hashedPass = Utilities.CreateHash(newUser.Password);

            var addedUser = new AppUser
            {
                Email = newUser.Email,
                HashedPassword = hashedPass,
            };

            _repo.Add(addedUser);

            return Ok(_repo.GetByUserEmail(newUser.Email));
        }
    }
}