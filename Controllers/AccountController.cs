using JWTAuthorizationASPNETCoreDemo.Models;
using JWTAuthorizationASPNETCoreDemo.Services;
using JWTAuthorizationASPNETCoreDemo.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthorizationASPNETCoreDemo.Controllers
{
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
        [Authorize]
        public IActionResult GetAllUsers()
        {
            return Ok(_repo.GetAllUsers());
        }
    }
}