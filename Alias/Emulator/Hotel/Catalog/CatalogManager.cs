using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Hotel.Catalog
{
	public class CatalogManager
	{
		private static List<CatalogPage> pages;
		private static List<CatalogFeatured> featured;

		public static void Initialize()
		{
			pages = CatalogDatabase.ReadPages();
			featured = CatalogDatabase.ReadFeatured();
		}

		public static List<CatalogFeatured> GetFeaturedPages()
		{
			return featured;
		}

		public static List<CatalogPage> GetCatalogPages(int pageId, Habbo habbo)
		{
			return pages.Where(page => page.ParentId == pageId && page.Visible && page.Rank <= habbo.Rank).ToList();
		}

		public static CatalogPage GetCatalogPage(int pageId)
		{
			return pages.Where(page => page.Id == pageId).FirstOrDefault();
		}
	}
}
