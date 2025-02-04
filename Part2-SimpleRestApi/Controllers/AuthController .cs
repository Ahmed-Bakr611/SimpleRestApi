using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Part2_SimpleRestApi.Helpers;
using Part2_SimpleRestApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Unicode;
using System.Text;

namespace Part2_SimpleRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtHelper _jwtHelper;

        public AuthController(JwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }

        [HttpPost("token")]
        public IActionResult GenerateToken([FromBody] LoginRequest loginRequest)
        {

            // Validate user credentials (this is just a dummy check, replace with real validation)
            if (loginRequest.Username == "johnd" && loginRequest.Password == "m38rmF$")
            {
                var token = _jwtHelper.GenerateToken("1"); // Replace with actual user ID
                return Ok(new TokenResponse { Token = token });
            }
            return Unauthorized();
        }


        [Authorize]
        [HttpPost("refresh")]
        public IActionResult RefreshToken()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
 

            var token = _jwtHelper.GenerateToken(userId);
            return Ok(new TokenResponse {Token = token});
        }
    }
}
