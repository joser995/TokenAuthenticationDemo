using Microsoft.AspNetCore.Mvc;
using TokenAuthenticationDemo.TokenAuthentication;

namespace TokenAuthenticationDemo.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly ITokenManager _tokenManager;

        public AuthenticateController(ITokenManager tokenManager)
        {
            this._tokenManager = tokenManager;
        }

        public IActionResult Authenticate(string user, string pwd)
        {
            if (_tokenManager.Authenticate(user, pwd))
            {
                return Ok(new { Token = _tokenManager.NewToken(2, "WeatherForcastWebApp2") });
            }
            else 
            {
                ModelState.AddModelError("Unathorized", "Access is restricted.");
                return Unauthorized(ModelState);
            }
        }
    }
}
