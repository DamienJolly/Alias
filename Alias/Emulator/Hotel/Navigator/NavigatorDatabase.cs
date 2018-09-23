using System.Collections.Generic;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Navigator.Views;
using Alias.Emulator.Hotel.Rooms;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Navigator
{
	class NavigatorDatabase
	{
		public static Dictionary<string, List<INavigatorCategory>> ReadCategories()
		{
			Dictionary<string, List<INavigatorCategory>> result = new Dictionary<string, List<INavigatorCategory>>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `navigator_categories` ORDER BY `order_id` DESC"))
				{
					while (Reader.Read())
					{
						INavigatorCategory category = Alias.Server.NavigatorManager.NewCategory(Reader.GetString("type"));
						category.Id                 = Reader.GetInt32("id");
						category.QueryId            = Reader.GetString("query_id");
						category.Title              = Reader.GetString("title");
						category.ShowCollapsed      = Reader.GetBoolean("show_collapsed");
						category.ShowThumbnail      = Reader.GetBoolean("show_thumbnail");
						category.MinRank            = Reader.GetInt32("min_rank");

						if (!result.ContainsKey(Reader.GetString("type")))
						{
							result.Add(Reader.GetString("type"), new List<INavigatorCategory>());
						}
						result[Reader.GetString("type")].Add(category);
					}
				}
			}
			return result;
		}

		public static List<RoomData> ReadPublicRooms()
		{
			List<RoomData> rooms = new List<RoomData>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT	`id` FROM `room_data` WHERE `category` = '0'"))
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
