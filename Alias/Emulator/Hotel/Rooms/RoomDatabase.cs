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
