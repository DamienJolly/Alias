using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Rooms
{
	public class RoomDatabase
	{
		public static RoomData RoomData(int Id)
		{
			RoomData result = new RoomData();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", Id);
				DataRow row = dbClient.DataRow("SELECT * FROM `room_data` WHERE `id` = @id LIMIT 1");
				if (row != null)
				{
					result.Id = Id;
					result.Name = (string)row["name"];
					result.OwnerId = (int)row["owner"];
					result.DoorState = RoomManager.IntToDoor(int.Parse((string)row["door"]));
					result.MaxUsers = (int)row["max_users"];
					result.Description = (string)row["description"];
					result.TradeState = RoomManager.IntToTrade(int.Parse((string)row["trade"]));
					result.Likes = RoomDatabase.ReadLikes(Id);
					result.Rankings = (int)row["ranking"];
					result.Category = (int)row["category"];
					result.Tags = RoomDatabase.ReadTags(Id);
					result.Image = (string)row["image"];
					result.Password = (string)row["password"];
					result.ModelName = (string)row["model"];
					result.Settings = RoomDatabase.ReadSettings(Id);
				}
				row.Delete();
			}
			return result;
		}

		private static RoomSettings ReadSettings(int Id)
		{
			RoomSettings result = new RoomSettings();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", Id);
				DataRow row = dbClient.DataRow("SELECT * FROM `room_settings` WHERE `id` = @id LIMIT 1");
				if (row != null)
				{
					result.WhoMutes = (int)row["who_can_mute"];
					result.WhoBans = (int)row["who_can_ban"];
					result.WhoKicks = (int)row["who_can_kick"];
					result.ChatDistance = (int)row["chat_distance"];
					result.ChatFlood = (int)row["chat_flood"];
					result.ChatMode = (int)row["chat_mode"];
					result.ChatSize = (int)row["chat_size"];
					result.ChatSpeed = (int)row["chat_speed"];
					result.AllowPets = AliasEnvironment.ToBool((string)row["allow_pets"]);
					result.AllowPetsEat = AliasEnvironment.ToBool((string)row["allow_pets_eat"]);
					result.RoomBlocking = AliasEnvironment.ToBool((string)row["room_blocking"]);
					result.HideWalls = AliasEnvironment.ToBool((string)row["hide_walls"]);
					result.WallHeight = (int)row["wall_height"];
					result.FloorSize = (int)row["floor_size"];
				}
				row.Delete();
			}
			return result;
		}

		private static List<string> ReadTags(int Id)
		{
			List<string> tags = new List<string>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", Id);
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `room_tags` WHERE `id` = @id").Rows)
				{
					tags.Add((string)row["tag"]);
					row.Delete();
				}
			}
			return tags;
		}

		private static List<int> ReadLikes(int Id)
		{
			List<int> likes = new List<int>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", Id);
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `room_likes` WHERE `id` = @id").Rows)
				{
					likes.Add((int)row["user_id"]);
					row.Delete();
				}
			}
			return likes;
		}

		public static void SaveRoom(RoomData data)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", data.Id);
				dbClient.AddParameter("name", data.Name);
				dbClient.AddParameter("ownerId", data.OwnerId);
				dbClient.AddParameter("door", RoomManager.DoorToInt(data.DoorState) + "");
				dbClient.AddParameter("maxusers", data.MaxUsers);
				dbClient.AddParameter("description", data.Description);
				dbClient.AddParameter("trade", RoomManager.TradeToInt(data.TradeState) + "");
				dbClient.AddParameter("ranking", data.Rankings);
				dbClient.AddParameter("category", data.Category);
				dbClient.AddParameter("image", data.Image);
				dbClient.AddParameter("password", data.Password);
				dbClient.AddParameter("model", data.ModelName);
				dbClient.Query("UPDATE `room_data` SET `name` = @name, `owner` = @ownerId, `door` = @door, `max_users` = @maxusers, " +
					"`description` = @description, `trade` = @trade, `ranking` = @ranking, `category` = @category, " +
					"`image` = @image, `password` = @password, `model` = @model WHERE `id` = @id");
				dbClient.ClearParameters();
				dbClient.AddParameter("id", data.Id);
				dbClient.AddParameter("mute", data.Settings.WhoMutes);
				dbClient.AddParameter("kick", data.Settings.WhoKicks);
				dbClient.AddParameter("ban", data.Settings.WhoBans);
				dbClient.AddParameter("chatmode", data.Settings.ChatMode);
				dbClient.AddParameter("chatsize", data.Settings.ChatSize);
				dbClient.AddParameter("chatspeed", data.Settings.ChatSpeed);
				dbClient.AddParameter("chatflood", data.Settings.ChatFlood);
				dbClient.AddParameter("chatdistance", data.Settings.ChatDistance);
				dbClient.AddParameter("allowpets", AliasEnvironment.BoolToString(data.Settings.AllowPets));
				dbClient.AddParameter("allowpetseat", AliasEnvironment.BoolToString(data.Settings.AllowPetsEat));
				dbClient.AddParameter("roomblocking", AliasEnvironment.BoolToString(data.Settings.RoomBlocking));
				dbClient.AddParameter("hidewalls", AliasEnvironment.BoolToString(data.Settings.HideWalls));
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
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (DataRow row in dbClient.DataTable("SELECT `id` FROM `room_data`").Rows)
				{
					result.Add((int)row["id"]);
					row.Delete();
				}
			}
			return result;
		}

		public static List<int> UserRooms(int userId)
		{
			List<int> result = new List<int>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", userId);
				foreach (DataRow row in dbClient.DataTable("SELECT `id` FROM `room_data` WHERE `owner` = @id").Rows)
				{
					result.Add((int)row["id"]);
					row.Delete();
				}
			}
			return result;
		}

		public static bool RoomExists(int roomId)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", roomId);
				return dbClient.Int32("SELECT `id` FROM `room_data` WHERE `id` = @id") != 0;
			}
		}
	}
}
