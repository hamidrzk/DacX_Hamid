using System.ComponentModel.DataAnnotations;

namespace DacXAngular.Entities
{
  public class Member
  {
    public int Id { get; set; }
    public string Name { get; set; }
		[Required]
		public string Email { get; set; }
    //public List<Tweet> Tweets { get; set; } = new();

  }
}
