using System.ComponentModel.DataAnnotations;

namespace DacXAngular.Entities
{
  public class Member
  {
    public int Id { get; set; }
    public string Name { get; set; }
		[Required]
		public string Email { get; set; }
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }

	}
}
