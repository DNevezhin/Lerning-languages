using Microsoft.AspNetCore.Mvc;
using System;

namespace LerningLanguages.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult RegisterUser([FromQuery] string password)
        {
            try
            {
                if (password.Length < 6)
                {
                    throw new ArgumentException("Пароль слишком короткий");
                }

                return Ok(new { message = "Пользователь успешно зарегистрирован" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
