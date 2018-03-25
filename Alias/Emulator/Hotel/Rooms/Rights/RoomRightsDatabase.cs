using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Users;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Rooms.Rights
{
    public class RoomRightsDatabase
    {
		public static List<UserRight> ReadRights(int Id)
		{
			List<UserRight> rights = new List<UserRight>();
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				dbClient.AddParameter("id", Id);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `room_rights` WHERE `id` = @id"))
				{
					while (Reader.Read())
					{
						UserRight right = new UserRight()
						{
							Id       = Reader.GetInt32("id"),
							Username = (string)UserDatabase.Variable(Reader.GetInt32("user_id"), "username")
						};
						rights.Add(right);
					}
				}
			}
			return rights;
		}

		public static void GiveRights(int Id, int UserId)
		{
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				dbClient.AddParameter("id", Id);
				dbClient.AddParameter("userId", UserId);
				dbClient.Query("INSERT INTO `room_rights` (`id`, `user_id`) VALUES (@id, @userId)");
			}
		}

		public static void TakeRights(int Id, int UserId)
		{
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				dbClient.AddParameter("id", Id);
				dbClient.AddParameter("userId", UserId);
				dbClient.Query("DELETE FROM `room_rights` WHERE `id` = @id AND `user_id` = @userId");
			}
		}
	}
}
