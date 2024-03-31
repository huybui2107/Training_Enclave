using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE.Service
{
    public class TokenService : ITokenService
    {
        private readonly string _jwtSecretKey;

        public TokenService(IConfiguration configuration)
        {
            _jwtSecretKey = configuration["JWT"];
        }
        public async Task<string> GenerateToken(string username)
        {
            var claims = new List<Claim>
           {
               new Claim(ClaimTypes.Email, username)
           };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenOptions = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
