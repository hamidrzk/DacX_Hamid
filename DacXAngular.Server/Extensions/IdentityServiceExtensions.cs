using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NetCoreAPI1.Extensions
{
  public static class IdentityServiceExtensions
  {
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          var tokenKey = Encoding.UTF8.GetBytes(config["TokenKey"]);
          options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
            ValidateIssuer = false,
            ValidateAudience = false,
          };
        });
      return services;
    }
  }
}
