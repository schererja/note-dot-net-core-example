using Eden.Core.Entities.Auth;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Eden.API.Services
{
    public class TokenService
    {
        private const int ExpirationMinutes = 30;
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string CreateToken(ApplicationUser user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
            var token = CreateJwtToken(CreateClaims(user), CreateSigningCredentials(), expiration);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(IEnumerable<Claim> claims, SigningCredentials signingCredentials, DateTime expiration)
        {
            return new JwtSecurityToken(_configuration["Authentication:Jwt:Issuer"], _configuration["Authentication:Jwt:Audience"], claims, expires: expiration, signingCredentials: signingCredentials);
        }

        private static IEnumerable<Claim> CreateClaims(ApplicationUser user)
        {

            try
            {
                if (user is { UserName: { }, Email: { } })
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, "TokenForTheApiWithAuth"),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email)
                    };
                    return claims;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return Array.Empty<Claim>();
        }
        private static SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c1d2e0a7-405b-40be-9b36-fa93469b673a")), SecurityAlgorithms.HmacSha256);
        }
    }
}
