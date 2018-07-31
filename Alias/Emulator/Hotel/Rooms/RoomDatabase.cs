using System.Collections.Generic;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Rooms
{
	class RoomDatabase
	{
		public static RoomData RoomData(int Id)
		{
			RoomData result = new RoomData();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", Id);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `room_data` WHERE `id` = @id LIMIT 1"))
				{
					while (Reader.Read())
					{
						result.Id          = Id;
						result.Name        = Reader.GetString("name");
						result.Group       = Alias.Server.GroupManager.GetGroup(Reader.GetInt32("group_id"));
						result.OwnerId     = Reader.GetInt32("owner");
						result.DoorState   = Alias.Server.RoomManager.IntToDoor(Reader.GetInt32("door"));
						result.MaxUsers    = Reader.GetInt32("max_users");
						result.Description = Reader.GetString("description");
						result.TradeState  = Alias.Server.RoomManager.IntToTrade(Reader.GetInt32("trade"));
						result.Likes       = RoomDatabase.ReadLikes(Id);
						result.Rankings    = Reader.GetInt32("ranking");
						result.Category    = Reader.GetInt32("category");
						result.Tags        = RoomDatabase.ReadTags(Id);
						result.Image       = Reader.GetString("image");
						result.Password    = Reader.GetString("password");
						result.ModelName   = Reader.GetString("model");
						result.Settings    = RoomDatabase.ReadSettings(Id);
					}
				}
			}
			return result;
		}

		private static RoomSettings ReadSettings(int Id)
		{
			RoomSettings result = new RoomSettings();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", Id);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `room_settings` WHERE `id` = @id LIMIT 1"))
				{
					while (Reader.Read())
					{
						result.WhoMutes     = Reader.GetInt32("who_can_mute");
						result.WhoBans      = Reader.GetInt32("who_can_ban");
						result.WhoKicks     = Reader.GetInt32("who_can_kick");
						result.ChatDistance = Reader.GetInt32("chat_distance");
						result.ChatFlood    = Reader.GetInt32("chat_flood");
						result.ChatMode     = Reader.GetInt32("chat_mode");
						result.ChatSize     = Reader.GetInt32("chat_size");
						result.ChatSpeed    = Reader.GetInt32("chat_speed");
						result.AllowPets    = Reader.GetBoolean("allow_pets");
						result.AllowPetsEat = Reader.GetBoolean("allow_pets_eat");
						result.RoomBlocking = Reader.GetBoolean("room_blocking");
						result.HideWalls    = Reader.GetBoolean("hide_walls");
						result.WallHeight   = Reader.GetInt32("wall_height");
						result.FloorSize    = Reader.GetInt32("floor_size");
					}
				}
			}
			return result;
		}

		private static List<string> ReadTags(int Id)
		{
			List<string> tags = new List<string>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", Id);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `room_tags` WHERE `id` = @id"))
				{
					while (Reader.Read())
					{
						tags.Add(Reader.GetString("tag"));
					}
				}
			}
			return tags;
		}

		private static List<int> ReadLikes(int Id)
		{
			List<int> likes = new List<int>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", Id);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `room_likes` WHERE `id` = @id"))
				{
					while (Reader.Read())
					{
						likes.Add(Reader.GetInt32("user_id"));
					}
				}
			}
			return likes;
		}

		public static int CreateRoom(int ownerId, string name, string description, string modelName, int maxUsers, int tradeType, int categoryId)
		{
			int roomId = 0;
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("name", name);
				dbClient.AddParameter("owner", ownerId);
				dbClient.AddParameter("maxusers", maxUsers);
				dbClient.AddParameter("description", description);
				dbClient.AddParameter("trade", tradeType.ToString());
				dbClient.AddParameter("category", categoryId);
				dbClient.AddParameter("model", modelName);
				dbClient.Query("INSERT INTO `room_data` (`name`, `owner`, `max_users`, `description`, `trade`, `category`, `model`) VALUES (@name, @owner, @maxusers, @description, @trade, @category, @model)");
				roomId = dbClient.LastInsertedId();

				dbClient.AddParameter("roomId", roomId);
				dbClient.Query("INSERT INTO `room_settings` (`id`) VALUES (@roomId)");
			}
			return roomId;
		}

		public static void SaveRoom(RoomData data)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", data.Id);
				dbClient.AddParameter("name", data.Name);
				dbClient.AddParameter("ownerId", data.OwnerId);
				dbClient.AddParameter("door", Alias.Server.RoomManager.DoorToInt(data.DoorState) + "");
				dbClient.AddParameter("maxusers", data.MaxUsers);
				dbClient.AddParameter("description", data.Description);
				dbClient.AddParameter("trade", Alias.Server.RoomManager.TradeToInt(data.TradeState) + "");
				dbClient.AddParameter("ranking", data.Rankings);
				dbClient.AddParameter("category", data.Category);
				dbClient.AddParameter("image", data.Image);
				dbClient.AddParameter("password", data.Password);
				dbClient.AddParameter("model", data.ModelName);
				dbClient.Query("UPDATE `room_data` SET `name` = @name, `owner` = @ownerId, `door` = @door, `max_users` = @maxusers, " +
					"`description` = @description, `trade` = @trade, `ranking` = @ranking, `category` = @category, " +
					"`image` = @image, `password` = @password, `model` = @model WHERE `id` = @id");

				dbClient.AddParameter("id", data.Id);
				dbClient.AddParameter("mute", data.Settings.WhoMutes);
				dbClient.AddParameter("kick", data.Settings.WhoKicks);
				dbClient.AddParameter("ban", data.Settings.WhoBans);
				dbClient.AddParameter("chatmode", data.Settings.ChatMode);
				dbClient.AddParameter("chatsize", data.Settings.ChatSize);
				dbClient.AddParameter("chatspeed", data.Settings.ChatSpeed);
				dbClient.AddParameter("chatflood", data.Settings.ChatFlood);
				dbClient.AddParameter("chatdistance", data.Settings.ChatDistance);
				dbClient.AddParameter("allowpets", Alias.BoolToString(data.Settings.AllowPets));
				dbClient.AddParameter("allowpetseat", Alias.BoolToString(data.Settings.AllowPetsEat));
				dbClient.AddParameter("roomblocking", Alias.BoolToString(data.Settings.RoomBlocking));
				dbClient.AddParameter("hidewalls", Alias.BoolToString(data.Settings.HideWalls));
				dbClient.AddParameter("wallheight", data.Settings.WallHeight);
				dbClient.AddParameter("floorsize", data.Settings.FloorSize);
				dbClient.Query("UPDATE `room_settings` SET `who_can_mute` = @mute, `who_can_kick` = @kick, `who_can_ban` = @ban, `chat_mode` = @chatmode, " +
					"`chat_size` = @chatsize, `chat_speed` = @chatspeed, `chat_flood` = @chatflood, `chat_distance` = @chatdistance, " +
					"`allow_pets` = @allowpets, `allow_pets_eat` = @allowpetseat, `room_blocking` = @roomblocking, `hide_walls` = @hidewalls, " +
					"`wall_height` = @wallheight, `floor_size` = @floorsize WHERE `id` = @id");
			}
		}

		public static List<int> AllRooms()
		{
			List<int> result = new List<int>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `id` FROM `room_data`"))
				{
					while (Reader.Read())
					{
						result.Add(Reader.GetInt32("id"));
					}
				}
			}
			return result;
		}

		public static List<int> UserRooms(int userId)
		{
			List<int> result = new List<int>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `id` FROM `room_data` WHERE `owner` = @id"))
				{
					while (Reader.Read())
					{
						result.Add(Reader.GetInt32("id"));
					}
				}
			}
			return result;
		}

		public static bool RoomExists(int roomId)
		{
			bool exists = false;
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", roomId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `id` FROM `room_data` WHERE `id` = @id"))
				{
					while (Reader.Read())
					{
						exists = true;
					}
				}
			}
			return exists;
		}
	}
}
