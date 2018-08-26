using System;
using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Hotel.Items;
using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Hotel.Users.Currency.Composers;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Users.Inventory.Composers;

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

		public List<int> GetAvailableNumbers(List<int> takenNumbers, int size)
		{
			List<int> availableNumbers = new List<int>();
			for (int i = 1; i <= size; i++)
			{
				if (!takenNumbers.Contains(i))
				{
					availableNumbers.Add(i);
				}
			}
			return availableNumbers;
		}

		public void PurchaseItem(CatalogPage page, CatalogItem item, Habbo habbo, int amount, string extradata)
		{
			if (item == null)
			{
				habbo.Session.Send(new AlertPurchaseFailedComposer(AlertPurchaseFailedComposer.SERVER_ERROR));
				return;
			}

			if (item.ClubLevel > habbo.ClubLevel)
			{
				habbo.Session.Send(new AlertPurchaseUnavailableComposer(AlertPurchaseUnavailableComposer.REQUIRES_CLUB));
				return;
			}

			if (amount <= 0 || amount > 100)
			{
				habbo.Session.Send(new AlertPurchaseFailedComposer(AlertPurchaseFailedComposer.SERVER_ERROR));
				return;
			}

			try
			{
				if (amount > 1 && !item.HasOffer)
				{
					habbo.Session.Send(new AlertPurchaseFailedComposer(AlertPurchaseUnavailableComposer.ILLEGAL));
					return;
				}

				if (item.IsLimited)
				{
					amount = 1;
					if (item.LimitedNumbers.Count <= 0)
					{
						habbo.Session.Send(new AlertLimitedSoldOutComposer());
						return;
					}
				}

				List<InventoryItem> itemsList = new List<InventoryItem>();
				int totalCredits = 0;
				int totalPoints = 0;
				int limitedNumber = 0;
				int limitedStack = 0;

				if (item.IsLimited)
				{
					limitedNumber = item.GetNumber;
					limitedStack = item.LimitedStack;
				}
				
				for (int i = 0; i < amount; i++)
				{
					if (item.Credits <= habbo.Credits - totalCredits)
					{
						if (item.Points <= habbo.Currency.GetCurrencyType(item.PointsType).Amount - totalPoints)
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
									if (baseItem.Interaction == ItemInteraction.BOT)
									{
										CatalogBots botData = GetCatalogBot(baseItem.Id);
										if (botData != null)
										{
											InventoryBots habboBot = new InventoryBots
											{
												Id = 0,
												Name = botData.Name,
												Motto = botData.Motto,
												Look = botData.Look,
												Gender = botData.Gender
											};
											habbo.Inventory.AddBot(habboBot);
											habbo.Session.Send(new AddBotComposer(habboBot));
										}
									}
									else
									{
										if (baseItem.Interaction == ItemInteraction.TROPHY)
										{
											extradata = habbo.Username + (char)9 + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + (char)9 + extradata;
										}

										if (baseItem.Interaction == ItemInteraction.TELEPORT)
										{
											InventoryItem teleportOne = new InventoryItem
											{
												Id = 0,
												LimitedNumber = limitedNumber,
												LimitedStack = limitedStack,
												ItemData = Alias.Server.ItemManager.GetItemData(baseItem.Id),
												Mode = 0,
												UserId = habbo.Id,
												ExtraData = extradata
											};
											habbo.Inventory.AddItem(teleportOne);
											InventoryItem teleportTwo = new InventoryItem
											{
												Id = 0,
												LimitedNumber = limitedNumber,
												LimitedStack = limitedStack,
												ItemData = Alias.Server.ItemManager.GetItemData(baseItem.Id),
												Mode = 0,
												UserId = habbo.Id,
												ExtraData = teleportOne.Id.ToString()
											};
											habbo.Inventory.AddItem(teleportTwo);
											teleportOne.ExtraData = teleportTwo.Id.ToString();
											habbo.Inventory.UpdateItem(teleportOne);
											itemsList.Add(teleportOne);
											itemsList.Add(teleportTwo);
										}
										else
										{
											InventoryItem habboItem = new InventoryItem
											{
												Id = 0,
												LimitedNumber = limitedNumber,
												LimitedStack = limitedStack,
												ItemData = Alias.Server.ItemManager.GetItemData(baseItem.Id),
												Mode = 0,
												UserId = habbo.Id,
												ExtraData = extradata
											};
											habbo.Inventory.AddItem(habboItem);
											itemsList.Add(habboItem);
										}
									}
								}
							}
						}
					}
				}

				if (totalCredits > 0)
				{
					habbo.Credits -= totalCredits;
					habbo.Session.Send(new UserCreditsComposer(habbo));
				}

				if (totalPoints > 0)
				{
					habbo.Currency.GetCurrencyType(item.PointsType).Amount -= totalPoints;
					habbo.Session.Send(new UserPointsComposer(habbo.Currency.GetCurrencyType(item.PointsType).Amount, -totalPoints, item.PointsType));
				}

				if (item.IsLimited)
				{
					item.AddLimited(limitedNumber);
				}

				if (itemsList != null)
				{
					habbo.Session.Send(new AddHabboItemsComposer(itemsList));
				}

				habbo.Session.Send(new PurchaseOKComposer(item));
				habbo.Session.Send(new InventoryRefreshComposer());
			}
			catch
			{
				habbo.Session.Send(new AlertPurchaseFailedComposer(AlertPurchaseFailedComposer.SERVER_ERROR));
			}
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
