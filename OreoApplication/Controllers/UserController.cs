using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OreoAppBusinessLayer.IServices;
using OreoAppCommonLayer.Model;

namespace OreoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        IConfiguration configuration;

        public UserController(IUserBL userBL, IConfiguration configuration)
        {
            this.userBL = userBL;
            this.configuration = configuration;
        }

        [HttpPost("Register")]
        public IActionResult RegisterUser(UserRegistration register)
        {
            try
            {
                if (this.userBL.RegisterUser(register))
                {
                    return this.Ok(new { success = true, Message = "User record added successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new { success = false, Message = "User record is not added " });
                }
            }
            catch (Exception exception)
            {

                if (exception != null)
                {
                    return StatusCode(StatusCodes.Status409Conflict,
                        new { success = false, ErrorMessage = "Cannot insert duplicate Email values." });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = exception.Message });
                }
            }
        }

        [HttpPost("Login")]
        public ActionResult UserLogin(UserLogin login)
        {
            try
            {
                var result = this.userBL.Login(login);
                if (result != null)
                {
                    string token = GenrateJWTToken(result.Email, result.UserId);
                    return this.Ok(new
                    {
                        success = true,
                        Message = "User login successfully",
                        Data = result,
                        Token = token
                    });
                }
                else
                {
                    return this.NotFound(new { success = false, Message = "User login unsuccessfully" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, Message = e.Message });
            }
        }

        private string GenrateJWTToken(string email, long id)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Key"]));
            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            string userId = Convert.ToString(id);
            var claims = new List<Claim>
                        {
                            new Claim("email", email),
                            new Claim("id",userId),
                        };
            var tokenOptionOne = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptionOne);
            return token;
        }
    }
}
