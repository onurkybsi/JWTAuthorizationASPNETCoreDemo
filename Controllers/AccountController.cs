using System.Threading.Tasks;
using JWTAuthorizationASPNETCoreDemo.Models.Abstract;
using JWTAuthorizationASPNETCoreDemo.Models.Concrete;
using JWTAuthorizationASPNETCoreDemo.Models.Concrete.DbModels;
using JWTAuthorizationASPNETCoreDemo.Services.Abstract;
using JWTAuthorizationASPNETCoreDemo.Services.Abstract.Repositories;
using JWTAuthorizationASPNETCoreDemo.Services.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

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

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult SignIn([FromBody] SignInModel newUser)
        {
            string hashedPass = Utilities.CreateHash(newUser.Password);

            var addedUser = new AppUser
            {
                Email = newUser.Email,
                HashedPassword = hashedPass,
                Role = newUser.Role
            };

            _repo.Add(addedUser);

            return Ok(_repo.GetByFilter(u => u.Email == newUser.Email));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_repo.GetListByFilter(null));
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            string currentUserToken = await HttpContext.GetTokenAsync("access_token");

            var currentUser = _repo.GetByFilter(u => u.Token == currentUserToken);

            return Ok(currentUser);
        }
    }
}