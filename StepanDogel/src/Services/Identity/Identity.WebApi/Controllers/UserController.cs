using Identity.WebApi.Data;
using Identity.WebApi.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        [HttpPost("registration")]
        public async Task<AuthenticateRequest> Registation(AuthenticateRequest model)
        {
            var user = new IdentityUser() { UserName = model.Username };
            var resut = await _signInManager.UserManager.CreateAsync(user, model.Password);
            return model;

        }
        [HttpPost("authenticate")]
        public async Task<ActionResult> Authenticate(AuthenticateRequest model)
        {
            var user = _signInManager.UserManager.Users.FirstOrDefault(x => x.UserName == model.Username);
            if (user == null) return BadRequest();
            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
            if (!result.Succeeded)
                return StatusCode(500);
            return Ok();
        }
    }
}
