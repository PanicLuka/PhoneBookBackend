using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBookBackend.Entities;
using PhoneBookBackend.Helpers;
using PhoneBookBackend.Services;


namespace PhoneBookBackend.Controllers
{
    [ApiController]
    [Route("api/login")]
    

    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] UserLogin user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var savedUser = _userService.GetUserByEmail(user.Email);

            bool passwordVerified = _authenticationService.VerifiedPassword(user);

            if (user.Email == savedUser.Email && passwordVerified)
            {

                var tokenString = _authenticationService.GenerateToken(user);

                Token token = new Token(tokenString);

                return Ok(tokenString);
            }

            return Unauthorized();

        }
    }
}
