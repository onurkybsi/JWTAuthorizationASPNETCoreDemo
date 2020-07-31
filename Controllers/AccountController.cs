using System.Threading.Tasks;
using JWTAuthorizationASPNETCoreDemo.Models;
using JWTAuthorizationASPNETCoreDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthorizationASPNETCoreDemo.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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
    }
}