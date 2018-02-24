using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Navigator.Views;
using Alias.Emulator.Hotel.Rooms;

namespace Alias.Emulator.Hotel.Navigator
{
	public class NavigatorDatabase
	{
		public static NavigatorPreference Preference(int UserId)
		{
			NavigatorPreference preference = new NavigatorPreference();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", UserId);
				DataRow row = dbClient.DataRow("SELECT * FROM `navigator_preferences` WHERE `id` = @id");
				if (row != null)
				{
					preference.X = (int)row["x"];
					preference.Y = (int)row["y"];
					preference.Height = (int)row["height"];
					preference.Width = (int)row["width"];
					preference.ShowSearches = AliasEnvironment.ToBool((string)row["show_searches"]);
					preference.UnknownInt = (int)row["unknown_int"];
				}
				row.Delete();
			}
			return preference;
		}

		public static List<NavigatorSearches> ReadSavedSearches(int UserId)
		{
			List<NavigatorSearches> searches = new List<NavigatorSearches>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("userId", UserId);
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `habbo_saved_searches` WHERE `user_id` = @userId").Rows)
				{
					NavigatorSearches search = new NavigatorSearches()
					{
						Page = (string)row["page"],
						SearchCode = (string)row["search_code"]
					};
					searches.Add(search);
					row.Delete();
				}
			}
			return searches;
		}

		public static List<RoomData> ReadPublicRooms(int extraId)
		{
			List<RoomData> result = new List<RoomData>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("categoryId", extraId);
				foreach (DataRow row in dbClient.DataTable("SELECT `id` FROM `room_data` WHERE `category` = @categoryId").Rows)
				{
					result.Add(RoomManager.RoomData((int)row["Id"]));
					row.Delete();
				}
			}
			return result;
		}

		public static void SavePreferences(NavigatorPreference preference, int UserId)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("x", preference.X);
				dbClient.AddParameter("y", preference.Y);
				dbClient.AddParameter("width", preference.Width);
				dbClient.AddParameter("height", preference.Height);
				dbClient.AddParameter("showsearches", AliasEnvironment.BoolToString(preference.ShowSearches));
				dbClient.AddParameter("unknownInt", preference.UnknownInt);
				dbClient.AddParameter("id", UserId);
				dbClient.Query("DELETE FROM `navigator_preferences` WHERE `id` = @id; INSERT INTO `navigator_preferences` (`Id`, `x`, `y`, `width`, `height`, `show_searches`, `unknown_int`) VALUES (@id, @x, @y, @width, @height, @showsearches, @unknownInt);");
			}
		}

		public static void SaveSearches()
		{

		}

		public static List<INavigatorCategory> ReadCategories()
		{
			List<INavigatorCategory> result = new List<INavigatorCategory>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `navigator_categories`").Rows)
				{
					INavigatorCategory category = Navigator.NewCategory((string)row["type"]);
					category.Id = (string)row["id"];
					category.Title = (string)row["title"];
					category.ShowButtons = AliasEnvironment.ToBool((string)row["show_buttons"]);
					category.ShowCollapsed = AliasEnvironment.ToBool((string)row["show_collapsed"]);
					category.ShowThumbnail = AliasEnvironment.ToBool((string)row["show_thumbnail"]);
					category.Type = (string)row["type"];
					category.OrderId = (int)row["order_id"];
					category.ExtraId = (int)row["extra_id"];
					category.MinRank = (int)row["min_rank"];
					category.Init();
					result.Add(category);
					row.Delete();
				}
			}
			return result;
		}
	}
}
