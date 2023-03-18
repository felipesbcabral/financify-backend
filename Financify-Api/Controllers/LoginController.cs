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
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<dynamic> Authenticate([FromBody] User model)
        {
            var user = _userRepository.Get(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "User of password invalid" });

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user,
                token,
            };
        }
    }
}
