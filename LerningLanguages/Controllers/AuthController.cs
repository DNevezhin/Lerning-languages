using LerningLanguages.Data;
using LerningLanguages.Models;
using LerningLanguages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace LerningLanguages.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context,JwtService jwtservice)
        {
            _context = context;
            _jwtService=jwtservice;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            if (dto.Password == null || dto.Login == null)
            {
                throw new ArgumentException("Введите логин и пароль");
            }
            if (dto.Password.Length < 6 || dto.Login.Length < 5)
            {
                throw new ArgumentException("Пароль или логин слишком короткий");
            }
            if (!dto.Password.Any(char.IsUpper))
            {
                throw new ArgumentException("Пароль должен содержать хотя бы 1 заглавную букву");
            }
            try
            {
                if (await _context.Users.AnyAsync(u => u.Login == dto.Login))
                {
                    return Conflict("Пользователь с таким логином уже существует");
                }
                else
                {
                    User user = new User
                    {
                        Login = dto.Login,
                        PasswordHash = dto.Password
                    };
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    return Ok(new { message = "Пользователь успешно зарегистрирован" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == dto.Login);
            if (user == null)
            {
                return Unauthorized("Пользователь не найден");
            }
            if (dto.Password != user.PasswordHash)
            {
                return Unauthorized("Неверный пароль");
            }

            var token=_jwtService.GenerateToken(user);
            return Ok(new { token = token });


        }
    }
}
