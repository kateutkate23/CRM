using API.DTO.User;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController(UserManager<User> userManager, 
        SignInManager<User> signInManager, ITokenHelper tokenHelper) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly ITokenHelper _tokenHelper = tokenHelper;

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == dto.Username);

            if (user == null)
            {
                return Unauthorized("Пользователь с таким именем не найден");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password ?? "", false);

            if (!result.Succeeded)
            {
                return Unauthorized("Пользователь не найден и/или пароль введен неверно");
            }

            return Ok(new NewUserDTO { UserName = user.UserName, Token = await _tokenHelper.CreateToken(user) });
        }

        ////admin added
        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var user = new User { UserName = dto.Username };

        //        var created = await _userManager.CreateAsync(user, dto.Password ?? "");
        //        if (created.Succeeded)
        //        {
        //            var result = await _userManager.AddToRoleAsync(user, "Admin");
        //            if (result.Succeeded)
        //            {
        //                return Ok(
        //                    new NewUserDTO
        //                    {
        //                        UserName = dto.Username,
        //                        Token = await _tokenHelper.CreateToken(user)
        //                    });
        //            }
        //            else
        //            {
        //                return StatusCode(500, result.Errors);
        //            }
        //        }
        //        else
        //        {
        //            return StatusCode(500, created.Errors);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex);
        //    }
        //}
    }
}
