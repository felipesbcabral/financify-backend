using Financify_Api.Models;
using Financify_Api.Repositories.Interfaces;
using Financify_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Financify_Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public LoginController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<dynamic> Authenticate([FromBody] Account model)
        {
            var account = _accountRepository.GetByEmail(model.Email);

            if (account == null)
                return NotFound(new { message = "User of password invalid" });

            var isValid = BCrypt.Net.BCrypt.Verify(model.Password, account.Password);

            if(!isValid) 
            {
               return BadRequest("Invalid email or password");
            }

            var token = TokenService.GenerateToken(account);

            account.Password = "";

            return new
            {
                account,
                token,
            };
        }
    }
}
