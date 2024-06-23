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

		[HttpGet("{id}")] // api/Members/id
		public Member Get(int id)
		{
			return _memberRepository.GetMemberData(id);
		}

		[HttpPost("save")] // api/Members/save
		public ActionResult<Member> Save([FromBody] Member member)
		{
			Member result = null;
			if (member == null || member.Id > 0)
			{
				return BadRequest("Invaid data!");
			}
			try
			{
				var m = _memberRepository.GetMemberByEmail(member.Email);
				if (m != null)
				{
					return BadRequest($"The email: [{member.Email}] is already used by another member");
				}
				result = _memberRepository.AddMember(member);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "error in Save member method!");
				return Problem("error in saving member!");
			}
			return Ok(result);
		}

		[HttpPut("update")] // api/Members/update
		public ActionResult<Member> Update([FromBody] Member member)
		{
			Member result = null;
			if (member == null || member.Id == 0)
			{
				return BadRequest("Invaid data!");
			}
			try
			{
				result = _memberRepository.GetMemberData(member.Id);
				if (result == null)
				{
					return base.NotFound("Memer not found");
				}
				int num = _memberRepository.UpdateMember(member);
				if (num > 0)
				{
					result = _memberRepository.GetMemberData(member.Id);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "error in Update member method!");
				return Problem("error in updating member!");
			}
			return result;
		}

		[HttpDelete("{id}")] // api/Members/id
		public ActionResult<int> Delete(int id)
		{
			int result = 0;
			var member = _memberRepository.GetMemberData(id);
			if (member == null)
			{
				return NotFound($"Couldn't fine a member with id:[{id}]");
			}
			result = _memberRepository.DeleteMember(member.Id);
			return Ok(result);
		}

		[HttpDelete("deleteByEmail")] // api/Members/deleteByEmail
		public ActionResult<int> DeleteByEmail([FromBody] Member m)
		{
			if( m== null || string.IsNullOrWhiteSpace(m.Email))
			{
				return BadRequest("Invalid email!");
			}
			int result = 0;
			var member = _memberRepository.GetMemberByEmail(m.Email);
			if (member == null)
			{
				return NotFound($"Couldn't fine a member with email:[{m.Email}]");
			}
			result = _memberRepository.DeleteMember(member.Id);
			return Ok(result);
		}
	}
}
