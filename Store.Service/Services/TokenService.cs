using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.Data.Entities.Identity;
using Store.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Store.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration )
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
        }

        public string GenerateUserToken(AppUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),    
                new Claim(ClaimTypes.GivenName ,user.DisplayName),
                new Claim("UserId",user.Id),
                new Claim("UserName" ,user.UserName),
            };

            var creds = new SigningCredentials(_key ,SecurityAlgorithms.HmacSha256);

            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["Token:Issuer"],
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandeler = new JwtSecurityTokenHandler();
            var token = tokenHandeler.CreateToken(tokenDiscriptor);
            return tokenHandeler.WriteToken(token);
        }
    }
}
