using System;
using System.Collections.Generic;
using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Hotel.Items;
using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Hotel.Users.Currency.Composers;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Catalog.Events
{
	class CatalogBuyItemEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int pageId = message.PopInt();
			int itemId = message.PopInt();
			string extraData = message.PopString();
			int amount = message.PopInt();

			CatalogPage page = Alias.Server.CatalogManager.GetCatalogPage(pageId);
			if (page == null)
			{
				session.Send(new AlertPurchaseFailedComposer(AlertPurchaseFailedComposer.SERVER_ERROR));
				return;
			}

			if (page.Rank > session.Habbo.Rank)
			{
				session.Send(new AlertPurchaseUnavailableComposer(AlertPurchaseUnavailableComposer.ILLEGAL));
				return;
			}

			CatalogItem item = page.GetCatalogItem(itemId);
			if (item == null)
			{
				session.Send(new AlertPurchaseFailedComposer(AlertPurchaseFailedComposer.SERVER_ERROR));
				return;
			}

			if (item.ClubLevel > session.Habbo.ClubLevel)
			{
				session.Send(new AlertPurchaseUnavailableComposer(AlertPurchaseUnavailableComposer.REQUIRES_CLUB));
				return;
			}

			if (amount <= 0 || amount > 100)
			{
				session.Send(new AlertPurchaseFailedComposer(AlertPurchaseFailedComposer.SERVER_ERROR));
				return;
			}

			if (amount > 1 && !item.HasOffer)
			{
				session.Send(new AlertPurchaseFailedComposer(AlertPurchaseUnavailableComposer.ILLEGAL));
				return;
			}

			if (item.IsLimited)
			{
				amount = 1;
				if (item.LimitedNumbers.Count <= 0)
				{
					session.Send(new AlertLimitedSoldOutComposer());
					return;
				}
			}

			int limitedNumber = 0;
			int limitedStack = 0;
			if (item.IsLimited)
			{
				limitedNumber = item.GetNumber;
				limitedStack = item.LimitedStack;
			}

			List<InventoryItem> itemsList = new List<InventoryItem>();
			int totalCredits = 0;
			int totalPoints = 0;

			for (int i = 0; i < amount; i++)
			{
				if (item.Credits <= session.Habbo.Credits - totalCredits)
				{
					if (item.Points <= session.Habbo.Currency.GetCurrencyType(item.PointsType).Amount - totalPoints)
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
									CatalogBots botData = Alias.Server.CatalogManager.GetCatalogBot(baseItem.Id);
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
										session.Habbo.Inventory.AddBot(habboBot);
										session.Send(new AddBotComposer(habboBot));
									}
								}
								else
								{
									if (baseItem.Interaction == ItemInteraction.TROPHY)
									{
										extraData = session.Habbo.Username + (char)9 + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + (char)9 + extraData;
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
											UserId = session.Habbo.Id,
											ExtraData = extraData
										};
										session.Habbo.Inventory.AddItem(teleportOne);
										InventoryItem teleportTwo = new InventoryItem
										{
											Id = 0,
											LimitedNumber = limitedNumber,
											LimitedStack = limitedStack,
											ItemData = Alias.Server.ItemManager.GetItemData(baseItem.Id),
											Mode = 0,
											UserId = session.Habbo.Id,
											ExtraData = teleportOne.Id.ToString()
										};
										session.Habbo.Inventory.AddItem(teleportTwo);
										teleportOne.ExtraData = teleportTwo.Id.ToString();
										session.Habbo.Inventory.UpdateItem(teleportOne);
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
											UserId = session.Habbo.Id,
											ExtraData = extraData
										};
										session.Habbo.Inventory.AddItem(habboItem);
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
				session.Habbo.Credits -= totalCredits;
				session.Send(new UserCreditsComposer(session.Habbo));
			}

			if (totalPoints > 0)
			{
				session.Habbo.Currency.GetCurrencyType(item.PointsType).Amount -= totalPoints;
				session.Send(new UserPointsComposer(session.Habbo.Currency.GetCurrencyType(item.PointsType).Amount, -totalPoints, item.PointsType));
			}

			if (item.IsLimited)
			{
				item.AddLimited(limitedNumber);
			}

			if (itemsList != null)
			{
				session.Send(new AddHabboItemsComposer(itemsList));
			}

			session.Habbo.AddPurchase(item);

			session.Send(new PurchaseOKComposer(item));
			session.Send(new InventoryRefreshComposer());
		}
	}
}
