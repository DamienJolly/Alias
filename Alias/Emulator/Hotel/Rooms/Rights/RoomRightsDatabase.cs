using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Hotel.Rooms.Rights
{
    public class RoomRightsDatabase
    {
		public static List<UserRight> ReadRights(int Id)
		{
			List<UserRight> rights = new List<UserRight>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", Id);
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `room_rights` WHERE `id` = @id").Rows)
				{
					UserRight right = new UserRight()
					{
						Id = (int)row["user_id"],
						Username = (string)UserDatabase.Variable((int)row["user_id"], "username")
					};
					rights.Add(right);
					row.Delete();
				}
			}
			return rights;
		}

		public static void GiveRights(int Id, int UserId)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", Id);
				dbClient.AddParameter("userId", UserId);
				dbClient.Query("INSERT INTO `room_rights` (`id`, `user_id`) VALUES (@id, @userId)");
			}
		}

		public static void TakeRights(int Id, int UserId)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", Id);
				dbClient.AddParameter("userId", UserId);
				dbClient.Query("DELETE FROM `room_rights` WHERE `id` = @id AND `user_id` = @userId");
			}
		}
	}
}
