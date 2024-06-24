using DacXAngular.Entities;
using DacXAngular.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace DacXAngular.Server.Data
{
	public class TweetRepository : ITweetRepository
	{
		private readonly string _connectionString;

		public TweetRepository(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("DAC");
		}

		public IEnumerable<Tweet> GetTopTweets(int top)
		{
			var list = new List<Tweet>();
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				string sqlQuery =
					@"SELECT TOP (@Top) T.[Id],T.[Message],T.[MemberId],T.[PostDate],M.[Email],M.[Name]
					FROM [Tweets] T INNER JOIN [Members] M ON T.[MemberId] = M.[Id]
					ORDER BY T.[Id] DESC";
				SqlCommand cmd = new SqlCommand(sqlQuery, con);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@Top", top);
				con.Open();
				SqlDataReader rdr = cmd.ExecuteReader();

				while (rdr.Read())
				{
					var tweet = FillTweet(rdr);
					list.Add(tweet);
				}
				con.Close();
			}
			return list;
		}

		public Tweet GetTweetData(int id)
		{
			Tweet tweet = null;

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				string sqlQuery =
					@"SELECT T.[Id],T.[Message],T.[MemberId],T.[PostDate],M.[Email],M.[Name]
					FROM [Tweets] T INNER JOIN [Members] M ON T.[MemberId] = M.[Id]
					WHERE T.[Id] = @Id"; 
				SqlCommand cmd = new SqlCommand(sqlQuery, con);
				cmd.Parameters.AddWithValue("@Id", id);
				con.Open();
				SqlDataReader rdr = cmd.ExecuteReader();

				while (rdr.Read())
				{
					tweet = FillTweet(rdr);
				}
			}
			return tweet;
		}

		private Tweet FillTweet(SqlDataReader rdr)
		{
			var sender = new Member()
			{
				Id = Convert.ToInt32(rdr["MemberId"]),
				Name = rdr["Name"].ToString(),
				Email = rdr["Email"].ToString()
			};
			var tweet = new Tweet()
			{
				Id = Convert.ToInt32(rdr["Id"]),
				Message = rdr["Message"].ToString(),
				MemberId = Convert.ToInt32(rdr["MemberId"]),
				PostDate = Convert.ToDateTime(rdr["PostDate"]),
				Sender = sender
			};
			return tweet;
		}

		public Tweet AddTweet(Tweet tweet)
		{
			Tweet result = null;
			int id = 0;
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				string sqlQuery =
					@"INSERT INTO [Tweets] ([Message],[MemberId],[PostDate])
					VALUES (@Message, @MemberId,@PostDate);
					SELECT SCOPE_IDENTITY()";
				SqlCommand cmd = new SqlCommand(sqlQuery, con);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@Message", tweet.Message);
				cmd.Parameters.AddWithValue("@MemberId", tweet.MemberId);
				cmd.Parameters.AddWithValue("@PostDate", DateTime.Now);
				con.Open();
				id = Convert.ToInt32(cmd.ExecuteScalar());
				con.Close();
			}
			if(id > 0)
			{
				result = GetTweetData(id);
			}
			return result;
		}

		public int UpdateTweet(Tweet tweet)
		{
			int result = 0;
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				string sqlQuery =
				@"UPDATE [Tweets] SET 
				[Message] = @Message,
				[PostDate] = @PostDate
				WHERE [Id] = @Id";
				SqlCommand cmd = new SqlCommand(sqlQuery, con);
				cmd.Parameters.AddWithValue("@Id", tweet.Id);
				cmd.Parameters.AddWithValue("@Message", tweet.Message);
				cmd.Parameters.AddWithValue("@PostDate", DateTime.Now);
				con.Open();
				result = cmd.ExecuteNonQuery();
				con.Close();
			}
			return result;
		}

		public int DeleteTweet(int id)
		{
			int result = 0;
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				string sqlQuery = @"DELETE FROM [dbo].[Tweets] WHERE Id = @Id";
				SqlCommand cmd = new SqlCommand(sqlQuery, con);
				cmd.Parameters.AddWithValue("@Id", id);
				con.Open();
				result = cmd.ExecuteNonQuery();
				con.Close();
			}
			return result;
		}
	}
}
