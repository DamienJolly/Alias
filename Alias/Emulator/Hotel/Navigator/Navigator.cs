using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Navigator.Views;

namespace Alias.Emulator.Hotel.Navigator
{
	public class Navigator
	{
		private static List<INavigatorCategory> Categories;

		public static void Initialize()
		{
			Categories = NavigatorDatabase.ReadCategories();
		}

		public static void Reload()
		{
			Categories.Clear();
			Initialize();
		}

		public static INavigatorCategory NewCategory(string type)
		{
			switch (type)
			{
				case "PUBLIC":
					{
						return new PublicCategory();
					}
				case "USER":
					{
						return new MyWorldCategory();
					}
				default:
					{
						return new NormalCategory();
					}
			}
		}

		public static List<INavigatorCategory> GetCategories(string type)
		{
			switch (type)
			{
				case "official_view":
					return Categories.Where(cat => cat.Type == "PUBLIC").OrderBy(cat => cat.OrderId).ToList();
				case "hotel_view":
					return Categories.Where(cat => cat.Type == "POPULAR").OrderBy(cat => cat.OrderId).ToList();
				case "roomads_view":
					return Categories.Where(cat => cat.Type == "EVENT").OrderBy(cat => cat.OrderId).ToList();
				case "myworld_view":
					return Categories.Where(cat => cat.Type == "USER").OrderBy(cat => cat.OrderId).ToList();
				default:
					return Categories.Where(cat => cat.Id == type).ToList();
			}
		}
	}
}
