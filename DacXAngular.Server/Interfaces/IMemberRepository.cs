using DacXAngular.Entities;

namespace DacXAngular.Interfaces
{
	public interface IMemberRepository
  {
		IEnumerable<Member> GetTopMembers(int top);
		public Member AddMember(Member user);
    public void UpdateMember(Member user);
    public Member GetMemberData(int id);
		public Member GetMemberByEmail(string email);
		public void DeleteMember(int? id);
	}
}
