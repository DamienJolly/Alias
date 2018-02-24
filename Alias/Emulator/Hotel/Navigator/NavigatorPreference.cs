using System.Collections.Generic;
using System.Linq;

namespace Alias.Emulator.Hotel.Navigator
{
	public class NavigatorPreference
	{
		public int X = 70;
		public int Y = 40;
		public int Width = 425;
		public int Height = 590;
		public bool ShowSearches = false;
		public int UnknownInt = 0;

		public List<NavigatorSearches> NavigatorSearches
		{
			get; set;
		} = new List<NavigatorSearches>();

		public bool HasPage(string page, string searchCode)
		{
			return this.NavigatorSearches.Where(x => x.Page == page && x.SearchCode == searchCode).Count() != 0;
		}

		public void RemoveSearch(int id)
		{
			if (this.NavigatorSearches.Count >= id && id != 0)
				this.NavigatorSearches.RemoveAt(id - 1);
		}

		public void AddSearch(string page, string searchCode)
		{
			NavigatorSearches search = new NavigatorSearches()
			{
				Page = page,
				SearchCode = searchCode
			};

			this.NavigatorSearches.Add(search);
		}

		public NavigatorPreference()
		{

		}
	}
}
