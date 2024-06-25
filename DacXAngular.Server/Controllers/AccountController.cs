using DacXAngular.DTOs;
using DacXAngular.Entities;
using DacXAngular.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace DacXAngular.Controllers
{
	public class AccountController : BaseApiController
  {
		private readonly IMemberRepository _memberRepository;
		private readonly ITokenService _tokenService;

    public AccountController(IMemberRepository memberRepository, ITokenService tokenService)
    {
			_memberRepository = memberRepository;
      _tokenService = tokenService;
    }

    [HttpPost("register")] // POST: api/account/register
    public ActionResult<MemberDto> Register(MemberDto registerDto)
    {
      if ( UserExists(registerDto.Email))
      {
        return BadRequest("Email Address is already taken");
      }

      using var hmac = new HMACSHA512();
      var member = new Member
      {
        Email = registerDto.Email,
        Name = registerDto.Name,
        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        PasswordSalt = hmac.Key
      };
      _memberRepository.AddMember(member);
      return MakeDto(member);
    }

    private bool UserExists(string email)
    {
      var m = _memberRepository.GetMemberByEmail(email);
      return m != null;
    }

    [HttpPost("login")]
    public ActionResult<MemberDto> Login(MemberDto memberDto)
    {
      var member = _memberRepository.GetMemberByEmail(memberDto.Email);
      if(member == null)
      {
        return Unauthorized("Invalid Email!");
      }
      if (member.PasswordSalt != null || member.PasswordHash != null)
      {
        using var hmac = new HMACSHA512(member.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(memberDto.Password));
        if (!computedHash.SequenceEqual(member.PasswordHash))
        {
          return Unauthorized("Invalid Password!");
        }
      }
      return MakeDto(member);
    }

    private MemberDto MakeDto(Member member)
    {
      if (member == null)
      {
        return null;
      }
			return new MemberDto
			{
				Id = member.Id,
				Email = member.Email,
				Name = member.Name,
				Token = _tokenService.CreateToken(member)
			};
		}
  }
}
