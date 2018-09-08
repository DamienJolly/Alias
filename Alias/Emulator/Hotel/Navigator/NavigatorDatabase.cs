using System.Collections.Generic;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Navigator.Views;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Utilities;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Navigator
{
	class NavigatorDatabase
	{
		public static NavigatorPreference Preference(int UserId)
		{
			NavigatorPreference preference = new NavigatorPreference();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", UserId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `navigator_preferences` WHERE `id` = @id"))
				{
					if (Reader.Read())
					{
						preference.X            = Reader.GetInt32("x");
						preference.Y            = Reader.GetInt32("y");
						preference.Height       = Reader.GetInt32("height");
						preference.Width        = Reader.GetInt32("width");
						preference.ShowSearches = Reader.GetBoolean("show_searches");
						preference.UnknownInt   = Reader.GetInt32("unknown_int");
					}
				}
			}
			return preference;
		}

		public static List<NavigatorSearches> ReadSavedSearches(int UserId)
		{
			List<NavigatorSearches> searches = new List<NavigatorSearches>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("userId", UserId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `habbo_saved_searches` WHERE `user_id` = @userId"))
				{
					while (Reader.Read())
					{
						NavigatorSearches search = new NavigatorSearches()
						{
							Page       = Reader.GetString("page"),
							SearchCode = Reader.GetString("search_code")
						};
						searches.Add(search);
					}
				}
			}
			return searches;
		}

		public static void SavePreferences(NavigatorPreference preference, int UserId)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("x", preference.X);
				dbClient.AddParameter("y", preference.Y);
				dbClient.AddParameter("width", preference.Width);
				dbClient.AddParameter("height", preference.Height);
				dbClient.AddParameter("showsearches", DatabaseBoolean.GetStringFromBool(preference.ShowSearches));
				dbClient.AddParameter("unknownInt", preference.UnknownInt);
				dbClient.AddParameter("id", UserId);
				dbClient.Query("REPLACE INTO `navigator_preferences` (`Id`, `x`, `y`, `width`, `height`, `show_searches`, `unknown_int`) VALUES (@id, @x, @y, @width, @height, @showsearches, @unknownInt)");
			}
		}

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
