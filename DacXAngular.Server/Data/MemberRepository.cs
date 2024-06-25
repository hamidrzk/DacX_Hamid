using DacXAngular.Entities;
using DacXAngular.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DacXAngular.Server.Data
{
	public class MemberRepository : IMemberRepository
	{
		private readonly string _connectionString;

		public MemberRepository(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("DAC");
		}

		public IEnumerable<Member> GetTopMembers(int top)
		{
			List<Member> lstMember = new List<Member>();
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				string sqlQuery = "SELECT TOP(@Top) * FROM [Members] ORDER BY [Id] DESC";
				SqlCommand cmd = new SqlCommand(sqlQuery, con);
				cmd.Parameters.AddWithValue("@Top", top);
				con.Open();
				SqlDataReader rdr = cmd.ExecuteReader();

				while (rdr.Read())
				{
					Member user = new Member();
					user.Id = Convert.ToInt32(rdr["Id"]);
					user.Name = rdr["Name"].ToString();
					user.Email = rdr["Email"].ToString();
					lstMember.Add(user);
				}
				con.Close();
			}
			return lstMember;
		}

		public Member AddMember(Member member)
		{
			Member result = null;
			int id = 0;
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				string sqlQuery =
					@"INSERT INTO [Members] ([Name],[Email],[PasswordHash],[PasswordSalt])
					VALUES (@Name, @Email,@PasswordHash,@PasswordSalt) SELECT SCOPE_IDENTITY()";
				SqlCommand cmd = new SqlCommand(sqlQuery, con);
				cmd.Parameters.AddWithValue("@Name", member.Name);
				cmd.Parameters.AddWithValue("@Email", member.Email);
				cmd.Parameters.AddWithValue("@PasswordHash", member.PasswordHash);
				cmd.Parameters.AddWithValue("@PasswordSalt", member.PasswordSalt);
				con.Open();
				id = Convert.ToInt32(cmd.ExecuteScalar());
				con.Close();
			}
			if(id > 0)
			{
				result = GetMemberData(id);
			}
			return result;
		}

		public int UpdateMember(Member user)
		{
			int result = 0;
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				string sqlQuery =
				@"UPDATE [Members] SET 
				[Name] = @Name,
				[Email] = @Email,
				[PasswordHash] = @PasswordHash,
				[PasswordSalt] = @PasswordSalt,
				WHERE [Id] = @Id";
				SqlCommand cmd = new SqlCommand(sqlQuery, con);
				cmd.Parameters.AddWithValue("@Id", user.Id);
				cmd.Parameters.AddWithValue("@Name", user.Name);
				cmd.Parameters.AddWithValue("@Email", user.Email);
				cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
				cmd.Parameters.AddWithValue("@PasswordSalt", user.PasswordSalt);
				con.Open();
				result = cmd.ExecuteNonQuery();
				con.Close();
			}
			return result;
		}

		public Member GetMemberData(int id)
		{
			Member user = new Member();

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				string sqlQuery = "SELECT * FROM [Members] WHERE [Id] = @Id";
				SqlCommand cmd = new SqlCommand(sqlQuery, con);
				cmd.Parameters.AddWithValue("@Id", id);
				con.Open();
				SqlDataReader rdr = cmd.ExecuteReader();

				while (rdr.Read())
				{
					user.Id = Convert.ToInt32(rdr["Id"]);
					user.Name = rdr["Name"].ToString();
					user.Email = rdr["Email"].ToString();
				}
			}
			return user;
		}

		public Member GetMemberByEmail(string email)
		{
			Member member = null;

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				string sqlQuery = "SELECT * FROM [Members] WHERE [Email] = @Email";
				SqlCommand cmd = new SqlCommand(sqlQuery, con);
				cmd.Parameters.AddWithValue("@Email", email);
				con.Open();
				SqlDataReader rdr = cmd.ExecuteReader();

				if (rdr.Read())
				{
					member = new Member();
					member.Id = Convert.ToInt32(rdr["Id"]);
					member.Name = rdr["Name"].ToString();
					member.Email = rdr["Email"].ToString();
				}
			}
			return member;
		}

		public int DeleteMember(int id)
		{
			int result = 0;
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				string sqlQuery = @"DELETE FROM [dbo].[Members] WHERE Id = @Id";
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
