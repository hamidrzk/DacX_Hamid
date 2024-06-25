using DacXAngular.Interfaces;
using DacXAngular.Server.Data;
using DacXAngular.Services;

namespace DacXAngular.Extensions
{
	public static class ApplicationServiceExtensions
  {
    public static IServiceCollection AddApplicatioService(this IServiceCollection services, IConfiguration config)
    {
      services.AddCors();
      services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IMemberRepository, MemberRepository>();
			services.AddScoped<ITweetRepository, TweetRepository>();
			return services;
    }
  }
}
