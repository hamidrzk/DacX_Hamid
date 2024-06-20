using DacXAngular.Entities;

namespace DacXAngular.Interfaces
{
  public interface ITokenService
  {
    string CreateToken(Member user);
  }
}
