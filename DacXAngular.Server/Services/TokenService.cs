using DacXAngular.Entities;
using DacXAngular.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DacXAngular.Services
{
	public class TokenService : ITokenService
  {
    private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration config) 
    {
      _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
    }

    string ITokenService.CreateToken(Member member)
    {
      var claims = new List<Claim>
      {
        new Claim(JwtRegisteredClaimNames.NameId, member.Email)
      };
      var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        SigningCredentials = creds,
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(7),
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);

    }
  }
}
