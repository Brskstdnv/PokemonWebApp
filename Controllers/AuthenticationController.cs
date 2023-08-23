using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PokemonWebApp.Configurations;
using PokemonWebApp.Dto;
using PokemonWebApp.Models;

namespace PokemonWebApp.Controllers
{
    [Route(template:"api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthenticationController(UserManager<IdentityUser> userManager, JwtConfig jwtConfig)
        {
            _userManager = userManager;
            _jwtConfig = jwtConfig;
        }

        [HttpPost]
        [Route(template:"Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
        {
            //Validate incoming request
            if (ModelState.IsValid)
            {
                var user_exist = await _userManager.FindByEmailAsync(requestDto.Email);

                if(user_exist != null)
                {
                    return BadRequest(error: new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Email already exist"
                        }
                    });
                }

                //create a user
                var new_user = new IdentityUser()
                {
                    Email = requestDto.Email,
                    UserName = requestDto.Email
                };

                var is_created/*:IdentityResult ?*/ = await _userManager.CreateAsync(new_user, requestDto.Password);

                if(is_created.Succeeded)
                {
                    //generate token
                }
                return BadRequest(new AuthResult()
                {

                });

            }
            return BadRequest();

        }

    }
}
