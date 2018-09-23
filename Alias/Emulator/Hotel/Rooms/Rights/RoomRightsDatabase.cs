using System.Collections.Generic;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Rooms.Rights
{
    public class RoomRightsDatabase
    {
		public static List<UserRight> ReadRights(int Id)
		{
			List<UserRight> rights = new List<UserRight>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", Id);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `room_rights` WHERE `id` = @id"))
				{
					while (Reader.Read())
					{
						UserRight right = new UserRight()
						{
							Id       = Reader.GetInt32("id"),
							Username = ""
						};
						rights.Add(right);
					}
				}
			}
			return rights;
		}

		public static void GiveRights(int Id, int UserId)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", Id);
				dbClient.AddParameter("userId", UserId);
				dbClient.Query("INSERT INTO `room_rights` (`id`, `user_id`) VALUES (@id, @userId)");
			}
		}

		public static void TakeRights(int Id, int UserId)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", Id);
				dbClient.AddParameter("userId", UserId);
				dbClient.Query("DELETE FROM `room_rights` WHERE `id` = @id AND `user_id` = @userId");
			}
		}
	}
}
