using DacXAngular.Entities;
using DacXAngular.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

		[HttpGet("{id}")] // api/Tweets/id
		public Tweet Get(int id)
		{
			return _tweetRepository.GetTweetData(id);
		}

		[HttpPost("send")] // api/Tweets/send
		public ActionResult<Tweet> Send([FromBody] Tweet tweet)
		{
			Tweet result = null;
			var member = _memberRepository.GetMemberByEmail(tweet.Sender.Email);
			if (member == null)
			{
				return BadRequest($"A member with email:[{tweet.Sender.Email}] doesn't exist!");
			}
			tweet.MemberId = member.Id;
			result = _tweetRepository.AddTweet(tweet);
			return Ok(result);
		}



		[HttpPut("update")] // api/Tweets/update
		public Tweet Put([FromBody] Tweet tweet)
		{
			Tweet result = null;
			if (tweet != null && tweet.Id > 0)
			{
				int num = _tweetRepository.UpdateTweet(tweet);
				if (num > 0)
				{
					result = _tweetRepository.GetTweetData(tweet.Id);
				}
			}
			return result;
		}

		[HttpDelete("{id}")] // api/Tweets
		public int Delete(int id)
		{
			int result = 0;
			var tweet = _tweetRepository.GetTweetData(id);
			if (tweet != null)
			{
				result = _tweetRepository.DeleteTweet(id);
			}
			return result;
		}
	}
}
