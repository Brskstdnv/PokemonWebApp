using Microsoft.AspNetCore.DataProtection;

namespace PokemonWebApp.Configurations
{
    public class JwtConfig
    {
        public string Secret { get; set; }
    }
}
