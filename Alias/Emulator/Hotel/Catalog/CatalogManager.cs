using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Hotel.Catalog
{
	sealed class CatalogManager
	{
		private readonly CatalogDao _dao;

		private IList<CatalogPage> _pages;
		private IList<CatalogFeatured> _featured;
		private IList<CatalogBots> _bots;

		public CatalogManager(CatalogDao catalogDao)
		{
			_dao = catalogDao;

			_pages = new List<CatalogPage>();
			_featured = new List<CatalogFeatured>();
			_bots = new List<CatalogBots>();
		}

		public async Task Initialize()
		{
			if (_pages.Count > 0)
			{
				_pages.Clear();
			}

			if (_featured.Count > 0)
			{
				_featured.Clear();
			}

			if (_bots.Count > 0)
			{
				_bots.Clear();
			}

			_pages = await _dao.ReadPagesAsync();
			await _dao.ReadItemsAsync(this);
			_featured = await _dao.ReadFeaturedAsync();
			_bots = await _dao.ReadBotsAsync();
		}

		public IList<CatalogFeatured> GetFeaturedPages()
		{
			return _featured;
		}

		public IList<CatalogPage> GetCatalogPages(int pageId, Habbo habbo)
		{
			return _pages.Where(page => page.ParentId == pageId && page.Visible && page.Rank <= habbo.Rank).OrderBy(page => page.Order).ToList();
		}

		public CatalogPage GetCatalogPage(int pageId)
		{
			return _pages.Where(page => page.Id == pageId).FirstOrDefault();
		}

		public CatalogBots GetCatalogBot(int itemId)
		{
			return _bots.Where(bot => bot.ItemId == itemId).FirstOrDefault();
		}

		public CatalogPage GetCatalogPageByOffer(int offerId)
		{
			return this._pages.Where(page => page.OfferIds.Contains(offerId)).FirstOrDefault();
		}

		public Task AddLimitedAsync(int itemId, int number) => _dao.AddLimitedAsync(itemId, number);
	}
}
