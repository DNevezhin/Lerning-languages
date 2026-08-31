using Microsoft.AspNetCore.Mvc;
using System;

namespace LerningLanguages.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult RegisterUser([FromQuery] string password, [FromQuery] string login)
        {
            try
            {
                if (password.Length < 6 || login.Length < 5 )
                {
                    throw new ArgumentException("Пароль или логин слишком короткий");
                }
                if(!password.Any(char.IsUpper))
                {
                    throw new ArgumentException("Пароль должен содержать хотя бы 1 заглавную букву");
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
