using DacXAngular.Entities;

namespace DacXAngular.Interfaces
{
	public interface ITweetRepository
  {
		public IEnumerable<Tweet> GetTopTweets(int top);
		public Tweet GetTweetData(int id);
		public Tweet AddTweet(Tweet tweet);
		public void UpdateTweet(Tweet tweet);
		public void DeleteTweet(int? id);
	}
}
