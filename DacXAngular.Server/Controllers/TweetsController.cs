using DacXAngular.Entities;
using DacXAngular.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NetCoreAPI1.Controllers;

namespace DacXAngular.Server.Controllers
{
	[Route("api/[controller]")] // api/Tweets
	[ApiController]
	public class TweetsController : ControllerBase
	{
		private readonly ITweetRepository _tweetRepository;
		private readonly IMemberRepository _memberRepository;
		private readonly ILogger<TweetsController> _logger;

		public TweetsController(ITweetRepository tweetRepository,
			IMemberRepository memberRepository,
			ILogger<TweetsController> logger)
		{
			this._tweetRepository = tweetRepository;
			this._memberRepository = memberRepository;
			this._logger = logger;
		}

		[HttpGet(Name = "GetTopTweets")] // api/Tweets
		public IEnumerable<Tweet> Get()
		{
			return _tweetRepository.GetTopTweets(10);
		}

		[HttpPost("send")] // api/Tweets
		public Tweet Send(Tweet tweet)
		{
			Tweet result = null;
			var member = _memberRepository.GetMemberByEmail(tweet.Sender.Email);
			if (member == null) //if a member doesn't exist, it creates a new one
			{
				member = _memberRepository.AddMember(tweet.Sender);
			}
			if(member != null)
			{
				tweet.MemberId = member.Id;
				result = _tweetRepository.AddTweet(tweet);
			}
			return result;
		}
			
	}
}
