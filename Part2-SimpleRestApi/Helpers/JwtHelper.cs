using Microsoft.IdentityModel.Tokens;
using Part2_SimpleRestApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;

namespace Part2_SimpleRestApi.Helpers
{
    public class JwtHelper
    {
        private readonly JWT _jwtSettings;
        


        public JwtHelper(IConfiguration configuration)
        { 
            _jwtSettings = configuration.GetSection("JwtSettings").Get<JWT>()
                ?? throw new ArgumentNullException(nameof(configuration), "JWT settings are missing in the configuration.");
        }

        public string GenerateToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId)
                }),
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.DurationInDays),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            var tokenValidationParameters = new TokenValidationParameters
            
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero // Optional: Remove time skew tolerance
            };

            try
            {
                return tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            }
            catch (Exception ex)
            {
                // Log the exception (using a logging framework or console)
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return null;
            }
        }
    }
}
