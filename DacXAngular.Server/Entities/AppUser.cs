using System.ComponentModel.DataAnnotations;

namespace DacXAngular.Entities
{
	public class AppUser
  {
    public int Id { get; set; }
    [Required]
    public string Email { get; set; }
    public string Name { get; set; }

    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
  }
}
