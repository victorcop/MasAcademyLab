using MasAcademyLab.Service;
using MasAcademyLab.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MasAcademyLab.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly ILogingService _loginService;

        public LoginController(ILogingService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="user">Object of the type <see cref="UserModel"/></param>
        /// <returns>Bearer Token</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserModel user)
        {
            var token = await _loginService.ValidateUser(user);

            if (token == null)
            {
                return Unauthorized(new { message = "nombre de usuario o contraseña incorrectos" });
            }

            return Ok(new { token });
        }
    }
}
