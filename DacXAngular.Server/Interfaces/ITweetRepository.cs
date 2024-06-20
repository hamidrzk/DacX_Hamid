using DacXAngular.Entities;

namespace DacXAngular.Interfaces
{
	public interface ITweetRepository
  {
		public IEnumerable<Tweet> GetTopTweets(int top);
		public Tweet GetTweetData(int id);
		public Tweet AddTweet(Tweet tweet);
		public int UpdateTweet(Tweet tweet);
		public int DeleteTweet(int id);
	}
}
