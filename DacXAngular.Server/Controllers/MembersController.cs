using DacXAngular.Entities;
using DacXAngular.Interfaces;
using DacXAngular.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace DacXAngular.Server.Controllers
{
	[Route("api/[controller]")] // api/Members
	[ApiController]
	public class MembersController : ControllerBase
	{
		private readonly IMemberRepository _memberRepository;
		private readonly ILogger<MembersController> _logger;

		public MembersController(IMemberRepository memberRepository,
			ILogger<MembersController> logger)
		{
			this._memberRepository = memberRepository;
			this._logger = logger;
		}

		[HttpGet(Name = "GetTopMembers")] // api/Members
		public IEnumerable<Member> GetTopMembers()
		{
			return _memberRepository.GetTopMembers(10);
		}

		[HttpGet("{id}")] // api/Tweets/id
		public Member Get(int id)
		{
			return _memberRepository.GetMemberData(id);
		}



		[HttpPost()] // api/Members/send
		public Member Post([FromBody] Member member)
		{
			Member result = null;
			if(member != null)
			{
				result = _memberRepository.AddMember(member);
			}
			return result;
		}



		[HttpPut] // api/Members
		public Member Put([FromBody] Member member)
		{
			Member result = null;
			if (member != null && member.Id > 0)
			{
				int num = _memberRepository.UpdateMember(member);
				if (num > 0)
				{
					result = _memberRepository.GetMemberData(member.Id);
				}
			}
			return result;
		}

		[HttpDelete("{id}")] // api/Members
		public int Delete(int id)
		{
			int result = 0;
			var member = _memberRepository.GetMemberData(id);
			if (member != null)
			{
				result = _memberRepository.DeleteMember(id);
			}
			return result;
		}
	}
}
