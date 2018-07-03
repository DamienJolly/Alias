using System.Collections.Generic;
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
				dbClient.AddParameter("userId", group.OwnerId);
				dbClient.Query("INSERT INTO `group_members` (`user_id`, `group_id`, `join_date`, `rank`) " +
					"VALUES (@userId, @groupId, UNIX_TIMESTAMP(), 0)");
				dbClient.AddParameter("groupId", groupId);
				dbClient.AddParameter("roomId", group.RoomId);
				dbClient.Query("UPDATE `room_data` SET `group_id` = @groupId WHERE `id` = @roomId");
			}
			return groupId;
		}

		public static void SetMemberRank(int groupId, int userId, int rank)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("groupId", groupId);
				dbClient.AddParameter("userId", userId);
				dbClient.AddParameter("rank", rank);
				dbClient.Query("UPDATE `group_members` SET `rank` = @rank WHERE	`user_id` = @userId AND	`group_id` = @groupId");
			}
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
						group = new Group(Reader.GetInt32("id"), Reader.GetString("name"), Reader.GetString("desc"), Reader.GetInt32("owner_id"), Reader.GetInt32("created"),
							Reader.GetInt32("room_id"), Reader.GetInt32("colour1"), Reader.GetInt32("colour2"), Reader.GetString("badge"), TryGetMemmbers(Reader.GetInt32("id")));
					}
				}
			}
			return group;
		}

		public static List<GroupMember> TryGetMemmbers(int groupId)
		{
			List<GroupMember> members = new List<GroupMember>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("groupId", groupId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `habbos`.`username`, `habbos`.`look`, `group_members`.* FROM `group_members` " +
					"INNER JOIN `habbos` ON `group_members`.`user_id` = `habbos`.`id` WHERE `group_members`.`group_id` = @groupId"))
				{
					while (Reader.Read())
					{
						members.Add(new GroupMember(Reader.GetInt32("user_id"), Reader.GetString("username"), Reader.GetString("look"), Reader.GetInt32("join_date"), Reader.GetInt32("rank")));
					}
				}
			}
			return members;
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
