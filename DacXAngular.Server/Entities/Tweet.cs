using System.ComponentModel.DataAnnotations.Schema;

namespace DacXAngular.Entities
{
  [Table("Photos")]
  public class Tweet
  {
    public int Id { get; set; }
    public string Message { get; set; }
    public int MemberId { get; set; }
    public DateTime PostDate { get; set; }
    public Member Sender { get; set; }
  }
}