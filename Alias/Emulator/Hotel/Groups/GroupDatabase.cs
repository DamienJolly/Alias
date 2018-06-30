using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Groups
{
    class GroupDatabase
    {
		public static int CreateGroup(Group group)
		{
			int groupId = 0;
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("name", group.Name);
				dbClient.AddParameter("desc", group.Description);
				dbClient.AddParameter("badge", group.Badge);
				dbClient.AddParameter("userId", group.OwnerId);
				dbClient.AddParameter("roomId", group.RoomId);
				dbClient.AddParameter("col1", group.ColourOne);
				dbClient.AddParameter("col2", group.ColourTwo);
				dbClient.Query("INSERT INTO `groups` (`name`, `desc`, `badge`, `owner_id`, `created`, `room_id`, `colour1`, `colour2`) " +
					"VALUES (@name, @desc, @badge, @userId, UNIX_TIMESTAMP(), @roomId, @col1, @col2)");
				groupId = dbClient.LastInsertedId();
				dbClient.AddParameter("groupId", groupId);
				dbClient.AddParameter("roomId", group.RoomId);
				dbClient.Query("UPDATE `room_data` SET `group_id` = @groupId WHERE `id` = @roomId");
			}
			return groupId;
		}

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
						group = new Group(Reader.GetInt32("id"), Reader.GetString("name"), Reader.GetString("desc"), Reader.GetInt32("owner_id"), 0,
							Reader.GetInt32("room_id"), Reader.GetInt32("colour1"), Reader.GetInt32("colour2"), Reader.GetString("badge"));
					}
				}
			}
			return group;
		}

		public static void ReadBadgeParts(GroupManager manager)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `groups_badges_parts` ORDER BY `id`"))
				{
					while (Reader.Read())
					{
						manager.BadgeParts.Add(new GroupBadgeParts(Reader.GetInt32("id"), Reader.GetString("code"), Reader.GetString("code2"), Reader.GetString("type")));
					}
				}
			}
		}
	}
}
