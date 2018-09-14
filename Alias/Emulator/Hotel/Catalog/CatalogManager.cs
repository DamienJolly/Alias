using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Hotel.Catalog
{
	sealed class CatalogManager
	{
		private List<CatalogPage> _pages;
		private List<CatalogFeatured> _featured;
		private List<CatalogBots> _bots;

		public CatalogManager()
		{
			this._pages = new List<CatalogPage>();
			this._featured = new List<CatalogFeatured>();
			this._bots = new List<CatalogBots>();
		}

		public void Initialize()
		{
			if (this._pages.Count > 0)
			{
				this._pages.Clear();
			}

			if (this._featured.Count > 0)
			{
				this._featured.Clear();
			}

			if (this._bots.Count > 0)
			{
				this._bots.Clear();
			}

			this._pages = CatalogDatabase.ReadPages();
			CatalogDatabase.ReadItems(this);
			this._featured = CatalogDatabase.ReadFeatured();
			this._bots = CatalogDatabase.ReadBots();
		}

		public List<CatalogFeatured> GetFeaturedPages()
		{
			return this._featured;
		}

		public List<CatalogPage> GetCatalogPages(int pageId, Habbo habbo)
		{
			return this._pages.Where(page => page.ParentId == pageId && page.Visible && page.Rank <= habbo.Rank).OrderBy(page => page.Order).ToList();
		}

		public CatalogPage GetCatalogPage(int pageId)
		{
			return this._pages.Where(page => page.Id == pageId).FirstOrDefault();
		}

		public CatalogBots GetCatalogBot(int itemId)
		{
			return this._bots.Where(bot => bot.ItemId == itemId).FirstOrDefault();
		}

		public CatalogPage GetCatalogPageByOffer(int offerId)
		{
			return this._pages.Where(page => page.OfferIds.Contains(offerId)).FirstOrDefault();
		}
	}
}
