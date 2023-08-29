using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace PokemonWebApp.Dto
{
    public class UserLoginRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
