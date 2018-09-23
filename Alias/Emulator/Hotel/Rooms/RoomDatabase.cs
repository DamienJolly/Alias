using System.Collections.Generic;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Rooms.States;
using Alias.Emulator.Utilities;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Rooms
{
	class RoomDatabase
	{
		public static RoomData RoomData(int Id)
		{
			RoomData result = null;
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", Id);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `room_data` WHERE `id` = @id LIMIT 1"))
				{
					while (Reader.Read())
					{
						result = new RoomData
						{
							Id          = Id,
							Name        = Reader.GetString("name"),
							Group       = Alias.Server.GroupManager.GetGroup(Reader.GetInt32("group_id")),
							OwnerId     = Reader.GetInt32("owner"),
							OwnerName   = "",
							DoorState   = (RoomDoorState)Reader.GetInt32("door"),
							MaxUsers    = Reader.GetInt32("max_users"),
							Description = Reader.GetString("description"),
							TradeState  = (RoomTradeState)Reader.GetInt32("trade"),
							Likes       = RoomDatabase.ReadLikes(Id),
							Rankings    = Reader.GetInt32("ranking"),
							Category    = Reader.GetInt32("category"),
							Tags        = RoomDatabase.ReadTags(Id),
							Image       = Reader.GetString("image"),
							Password    = Reader.GetString("password"),
							ModelName   = Reader.GetString("model"),
							Settings    = RoomDatabase.ReadSettings(Id)
						};
					}
				}
			}
			return result;
		}

		private static RoomSettings ReadSettings(int Id)
		{
			RoomSettings result = null;
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", Id);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `room_settings` WHERE `id` = @id LIMIT 1"))
				{
					while (Reader.Read())
					{
						result = new RoomSettings
						{
							WhoMutes     = Reader.GetInt32("who_can_mute"),
							WhoBans      = Reader.GetInt32("who_can_ban"),
							WhoKicks     = Reader.GetInt32("who_can_kick"),
							ChatDistance = Reader.GetInt32("chat_distance"),
							ChatFlood    = Reader.GetInt32("chat_flood"),
							ChatMode     = Reader.GetInt32("chat_mode"),
							ChatSize     = Reader.GetInt32("chat_size"),
							ChatSpeed    = Reader.GetInt32("chat_speed"),
							AllowPets    = Reader.GetBoolean("allow_pets"),
							AllowPetsEat = Reader.GetBoolean("allow_pets_eat"),
							RoomBlocking = Reader.GetBoolean("room_blocking"),
							HideWalls    = Reader.GetBoolean("hide_walls"),
							WallHeight   = Reader.GetInt32("wall_height"),
							FloorSize    = Reader.GetInt32("floor_size")
						};
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
				dbClient.AddParameter("door", (int)data.DoorState + "");
				dbClient.AddParameter("maxusers", data.MaxUsers);
				dbClient.AddParameter("description", data.Description);
				dbClient.AddParameter("trade", (int)data.TradeState + "");
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
				dbClient.AddParameter("allowpets", DatabaseBoolean.GetStringFromBool(data.Settings.AllowPets));
				dbClient.AddParameter("allowpetseat", DatabaseBoolean.GetStringFromBool(data.Settings.AllowPetsEat));
				dbClient.AddParameter("roomblocking", DatabaseBoolean.GetStringFromBool(data.Settings.RoomBlocking));
				dbClient.AddParameter("hidewalls", DatabaseBoolean.GetStringFromBool(data.Settings.HideWalls));
				dbClient.AddParameter("wallheight", data.Settings.WallHeight);
				dbClient.AddParameter("floorsize", data.Settings.FloorSize);
				dbClient.Query("UPDATE `room_settings` SET `who_can_mute` = @mute, `who_can_kick` = @kick, `who_can_ban` = @ban, `chat_mode` = @chatmode, " +
					"`chat_size` = @chatsize, `chat_speed` = @chatspeed, `chat_flood` = @chatflood, `chat_distance` = @chatdistance, " +
					"`allow_pets` = @allowpets, `allow_pets_eat` = @allowpetseat, `room_blocking` = @roomblocking, `hide_walls` = @hidewalls, " +
					"`wall_height` = @wallheight, `floor_size` = @floorsize WHERE `id` = @id");
			}
		}

		public static List<RoomData> UserRooms(int userId)
		{
			List<RoomData> rooms = new List<RoomData>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `id` FROM `room_data` WHERE `owner` = @id"))
				{
					while (Reader.Read())
					{
						if (Alias.Server.RoomManager.TryGetRoomData(Reader.GetInt32("id"), out RoomData roomData))
						{
							rooms.Add(roomData);
						}
					}
				}
			}
			return rooms;
		}
	}
}
