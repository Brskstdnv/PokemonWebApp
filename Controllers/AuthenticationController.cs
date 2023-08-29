using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PokemonWebApp.Configurations;
using PokemonWebApp.Dto;
using PokemonWebApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PokemonWebApp.Controllers
{
    //[Route(template: "api/[controller]")]
    //[ApiController]
    //public class AuthenticationController : ControllerBase
    //{
    //    private readonly UserManager<IdentityUser> _userManager;
    //    private readonly IConfiguration _configuration;
    //    //private readonly JwtConfig _jwtConfig;

    //    public AuthenticationController(UserManager<IdentityUser> userManager, /*JwtConfig jwtConfig*/ IConfiguration configuration)
    //    {
    //        _userManager = userManager;
    //        _configuration = configuration;
    //        // _jwtConfig = jwtConfig;
    //    }

    //    [HttpPost]
    //    [Route(template: "Register")]
    //    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
    //    {
    //        //Validate incoming request
    //        if (ModelState.IsValid)
    //        {
    //            var user_exist = await _userManager.FindByEmailAsync(requestDto.Email);

    //            if (user_exist != null)
    //            {
    //                return BadRequest(error: new AuthResult()
    //                {
    //                    Result = false,
    //                    Errors = new List<string>()
    //                    {
    //                        "Email already exist"
    //                    }
    //                });
    //            }

    //            //create a user
    //            var new_user = new IdentityUser()
    //            {
    //                Email = requestDto.Email,
    //                UserName = requestDto.Email
    //            };

    //            var is_created/*:IdentityResult ?*/ = await _userManager.CreateAsync(new_user, requestDto.Password);

    //            if (is_created.Succeeded)
    //            {
    //                //generate token
    //                var token = GenerateJwtToken(new_user);

    //                return Ok(new AuthResult()
    //                {
    //                    Result = true,
    //                    Token = token,
    //                });
    //            }
    //            return BadRequest(new AuthResult()
    //            {
    //                Errors = new List<string>()
    //                {
    //                    "Server Error"
    //                },
    //                Result = false
    //            });

    //        }
    //        return BadRequest();

    //    }
    //    [HttpPost]
    //    [Route("Login")]
    //    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto loginRequest)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            //Check if the user exist
    //            var existing_user = await _userManager.FindByEmailAsync(loginRequest.Email);

    //            if (existing_user == null)
    //                return BadRequest(new AuthResult()
    //                {
    //                    Errors = new List<string>()
    //                    {
    //                        "Invalid payload"
    //                    },
    //                    Result = false
    //                });
    //            var isCorrect = await _userManager.CheckPasswordAsync(existing_user, loginRequest.Password);

    //            if (!isCorrect)
    //                return BadRequest(new AuthResult()
    //                {
    //                    Errors = new List<string>()
    //                    {
    //                        "Invalid credentials"
    //                    },
    //                    Result = false

    //                });
    //            var jwtToken = GenerateJwtToken(existing_user);

    //            return Ok(new AuthResult()
    //            {
    //                Token = jwtToken,
    //                Result = true
    //            });
    //        }
    //        return BadRequest(new AuthResult()
    //        {
    //            Errors = new List<string>()
    //            {
    //                "Invalid payload"
    //            },
    //            Result = false
    //        });
    //    }

    //    private string GenerateJwtToken(IdentityUser user)
    //    {
    //        var jwtTokenHandler = new JwtSecurityTokenHandler();

    //        var key = Encoding.UTF8.GetBytes(_configuration.GetSection(key: "Jwt:Key").Value);

    //        //Token description
    //        var tokenDescriptor = new SecurityTokenDescriptor()
    //        {
    //            Subject = new ClaimsIdentity(new[]
    //            {
    //                new Claim(type: "Id", value: user.Id),
    //                new Claim(type:JwtRegisteredClaimNames.Sub, value: user.Email),
    //                new Claim(type:JwtRegisteredClaimNames.Email,value:user.Email),
    //                new Claim(type:JwtRegisteredClaimNames.Jti, value:Guid.NewGuid().ToString()),
    //                new Claim(type:JwtRegisteredClaimNames.Iat, value:DateTime.Now.ToUniversalTime().ToString()),
    //            }),

    //            Expires = DateTime.Now.AddHours(1),
    //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256)
    //        };

    //        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
    //        return jwtTokenHandler.WriteToken(token);
    //    }

    //}
}
