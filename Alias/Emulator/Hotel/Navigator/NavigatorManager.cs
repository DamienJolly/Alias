using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Navigator.Views;

namespace Alias.Emulator.Hotel.Navigator
{
	sealed class NavigatorManager
	{
		private List<INavigatorCategory> _categories;

		public NavigatorManager()
		{
			this._categories = new List<INavigatorCategory>();
		}

		public void Initialize()
		{
			if (this._categories.Count > 0)
			{
				this._categories.Clear();
			}

			this._categories = NavigatorDatabase.ReadCategories();
		}
		
		public INavigatorCategory NewCategory(string type)
		{
			switch (type)
			{
				case "PUBLIC": return new PublicCategory();
				case "USER": return new MyWorldCategory();
				default: return new NormalCategory();
			}
		}

		public List<INavigatorCategory> GetCategories(string type)
		{
			switch (type)
			{
				case "official_view":
					return this._categories.Where(cat => cat.Type == "PUBLIC").OrderBy(cat => cat.OrderId).ToList();
				case "hotel_view":
					return this._categories.Where(cat => cat.Type == "POPULAR").OrderBy(cat => cat.OrderId).ToList();
				case "roomads_view":
					return this._categories.Where(cat => cat.Type == "EVENT").OrderBy(cat => cat.OrderId).ToList();
				case "myworld_view":
					return this._categories.Where(cat => cat.Type == "USER").OrderBy(cat => cat.OrderId).ToList();
				default:
					return this._categories.Where(cat => cat.Id == type).ToList();
			}
		}
	}
}
