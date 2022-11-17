using System.ComponentModel.DataAnnotations;

namespace Identity.WebApi.IdentityServer
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
