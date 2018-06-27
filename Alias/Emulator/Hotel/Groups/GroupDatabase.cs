using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Groups
{
    class GroupDatabase
    {
		public static Group TryGetGroup(int groupId)
		{
			Group group = null;
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("groupId", groupId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `groups` WHERE `id` = @groupId"))
				{
					if (Reader.Read())
					{
						group = new Group(Reader.GetInt32("id"), Reader.GetString("name"), Reader.GetString("desc"), Reader.GetInt32("owner_id"), 0, Reader.GetInt32("room_id"));
					}
				}
			}
			return group;
		}
	}
}
