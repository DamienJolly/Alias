using System.Collections.Generic;
using Alias.Emulator.Hotel.Navigator.Views;
using Alias.Emulator.Hotel.Rooms;

namespace Alias.Emulator.Hotel.Navigator
{
	class NavigatorManager
	{
		
		public Dictionary<string, List<INavigatorCategory>> Categories
		{
			get; set;
		}

		public List<RoomData> PublicRooms
		{
			get; set;
		}

		public NavigatorManager()
		{
			Categories = new Dictionary<string, List<INavigatorCategory>>();
			PublicRooms = new List<RoomData>();
		}

		public void Initialize()
		{
			if (Categories.Count > 0)
			{
				Categories.Clear();
			}

			Categories = NavigatorDatabase.ReadCategories();
			PublicRooms = NavigatorDatabase.ReadPublicRooms();
		}
		
		public INavigatorCategory NewCategory(string type)
		{
			switch (type)
			{
				case "official_view": return new PublicCategory();
				case "myworld_view": return new MyWorldCategory();
				case "roomads_view": return new EventsCategory();
				case "hotel_view": default: return new NormalCategory();
			}
		}

		public bool TryGetCategories(string type, out List<INavigatorCategory> categories)
		{
			return Categories.TryGetValue(type, out categories);
		}
	}
}
