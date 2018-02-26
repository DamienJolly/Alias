using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Hotel.Items;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Users.Inventory.Composers;

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

		public static void PurchaseItem(CatalogPage page, CatalogItem item, Habbo habbo, int amount, string extradata)
		{
			ItemData cBaseItem = null;
			if (item == null)
			{
				habbo.Session().Send(new AlertPurchaseFailedComposer(AlertPurchaseFailedComposer.SERVER_ERROR));
				return;
			}

			if (item.ClubLevel > habbo.ClubLevel)
			{
				habbo.Session().Send(new AlertPurchaseUnavailableComposer(AlertPurchaseUnavailableComposer.REQUIRES_CLUB));
				return;
			}

			if (amount <= 0 || amount > 100)
			{
				habbo.Session().Send(new AlertPurchaseFailedComposer(AlertPurchaseFailedComposer.SERVER_ERROR));
				return;
			}

			try
			{
				if (amount > 1 && !item.HasOffer)
				{
					habbo.Session().Send(new AlertPurchaseFailedComposer(AlertPurchaseUnavailableComposer.ILLEGAL));
					return;
				}

				if (item.IsLimited)
				{
					amount = 1;
					if (item.LimitedStack - item.LimitedSells <= 0)
					{
						habbo.Session().Send(new AlertLimitedSoldOutComposer());
						return;
					}
				}

				List<InventoryItem> itemsList = new List<InventoryItem>();
				int totalCredits = 0;
				int totalPoints = 0;

				for (int i = 0; i < amount; i++)
				{
					if (item.Credits <= habbo.Credits - totalCredits)
					{
						if (item.Points <= habbo.Points - totalPoints)
						{
							if (((i + 1) % 6 != 0 && item.HasOffer) || !item.HasOffer)
							{
								totalCredits += item.Credits;
								totalPoints += item.Points;
							}

							foreach (ItemData baseItem in item.GetItems())
							{
								for (int k = 0; k < item.GetItemAmount(baseItem.Id); k++)
								{
									cBaseItem = baseItem;
									InventoryItem habboItem = new InventoryItem();
									habboItem.Id = 0;
									habboItem.ItemData = ItemManager.GetItemData(baseItem.Id);
									itemsList.Add(habboItem);
								}
							}
						}
					}
				}

				if (totalCredits > 0)
				{
					//todo: take credits
				}

				if (itemsList != null)
				{
					habbo.Session().Send(new AddHabboItemsComposer(itemsList));
					habbo.Inventory().AddItems(itemsList);
					habbo.Session().Send(new PurchaseOKComposer(item));
					habbo.Session().Send(new InventoryRefreshComposer());
				}
			}
			catch
			{
				habbo.Session().Send(new AlertPurchaseFailedComposer(AlertPurchaseFailedComposer.SERVER_ERROR));
			}
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
