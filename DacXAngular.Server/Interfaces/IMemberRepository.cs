using DacXAngular.Entities;

namespace DacXAngular.Interfaces
{
	public interface IMemberRepository
  {
		IEnumerable<Member> GetTopMembers(int top);
		public Member AddMember(Member user);
    public int UpdateMember(Member user);
    public Member GetMemberData(int id);
		public Member GetMemberByEmail(string email);
		public int DeleteMember(int id);
	}
}
