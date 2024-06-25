using DacXAngular.DTOs;
using DacXAngular.Entities;
using DacXAngular.Interfaces;
using DacXAngular.Server.Data;
using DacXAngular.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DacXAngular.Server.Controllers
{
	[Route("api/[controller]")] // api/Members
	[ApiController]
	[Authorize]
	public class MembersController : ControllerBase
	{
		private readonly IMemberRepository _memberRepository;
		private readonly ILogger<MembersController> _logger;
		private readonly ITokenService _tokenService;


		public MembersController(IMemberRepository memberRepository,
			ITokenService tokenService,
			ILogger<MembersController> logger)
		{
			this._memberRepository = memberRepository;
			this._tokenService = tokenService;
			this._logger = logger;
		}

		[HttpGet(Name = "GetTopMembers")] // api/Members
		public IEnumerable<Member> GetTopMembers()
		{
			return _memberRepository.GetTopMembers(10);
		}

		[HttpGet("{id}")] // api/Members/id
		public ActionResult<MemberDto> Get(int id)
		{
			var member = _memberRepository.GetMemberData(id);
			if (member == null)
			{
				return NotFound("Not found");
			}
			return new MemberDto
			{
				Id = id,
				Email = member.Email,
				Name = member.Name,
			};
		}

		[HttpPost("save")] // POST: api/Members/register
		public ActionResult<MemberDto> Save([FromBody] MemberDto memberDto)
		{
			if (memberDto == null || memberDto.Id == 0)
			{
				return BadRequest("Bad request!");
			}
			var m = _memberRepository.GetMemberData(memberDto.Id);
			if (m == null)
			{
				return NotFound("Member not found!");
			}

			var member = MakeMember(memberDto);
			_memberRepository.AddMember(member);

			return new MemberDto
			{
				Email = member.Email,
				Name = member.Name,
				Token = _tokenService.CreateToken(member)
			};
		}

		private Member MakeMember(MemberDto registerDto)
		{
			using var hmac = new HMACSHA512();
			return new Member
			{
				Email = registerDto.Email,
				Name = registerDto.Name,
				PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
				PasswordSalt = hmac.Key
			};
		}

		[HttpPut("update")] // api/Members/update
		public ActionResult<MemberDto> Update([FromBody] MemberDto memberDto)
		{
			Member result = null;
			if (memberDto == null)
			{
				return BadRequest("Invaid data!");
			}
			try
			{
				if (memberDto.Id > 0)
				{
					result = _memberRepository.GetMemberData(memberDto.Id);
				}
				else if (memberDto.Email != null) 
				{
					result = _memberRepository.GetMemberByEmail(memberDto.Email);
				}
				if (result != null)
				{
					var member = MakeMember(memberDto);
					int num = _memberRepository.UpdateMember(member);
					if (num > 0)
					{
						result = _memberRepository.GetMemberData(member.Id);
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "error in Update member method!");
				return Problem("error in updating member!");
			}
			if (result == null)
			{
				return base.NotFound("Member not found");
			}
			return new MemberDto
			{
				Email = memberDto.Email,
				Name = memberDto.Name,
			};
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
			if (m == null || string.IsNullOrWhiteSpace(m.Email))
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
