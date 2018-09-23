using System;
using System.Collections.Generic;
using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Hotel.Items;
using Alias.Emulator.Hotel.Players.Currency.Composers;
using Alias.Emulator.Hotel.Players.Inventory;
using Alias.Emulator.Hotel.Players.Inventory.Composers;
using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Catalog.Events
{
	class CatalogBuyItemEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
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

			if (page.Rank > session.Player.Rank)
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

			if (item.ClubLevel > session.Player.ClubLevel)
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
				if (item.Credits <= session.Player.Credits - totalCredits)
				{
					//if (item.Points <= session.Player.Currency.TryGetCurrency(item.PointsType).Amount - totalPoints) todo:
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
										InventoryBot habboBot = new InventoryBot(-1, botData.Name, botData.Motto, botData.Look, botData.Gender);
										await session.Player.Inventory.AddBot(habboBot);
										session.Send(new AddBotComposer(habboBot));
									}
								}
								else
								{
									if (baseItem.Interaction == ItemInteraction.TROPHY)
									{
										extraData = session.Player.Username + (char)9 + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + (char)9 + extraData;
									}

									if (baseItem.Interaction == ItemInteraction.TELEPORT)
									{
										InventoryItem teleportOne = new InventoryItem(-1, limitedNumber, limitedStack, baseItem.Id, session.Player.Id, extraData);
										await session.Player.Inventory.AddItem(teleportOne);
										InventoryItem teleportTwo = new InventoryItem(-1, limitedNumber, limitedStack, baseItem.Id, session.Player.Id, teleportOne.Id.ToString());
										await session.Player.Inventory.AddItem(teleportTwo);
										teleportOne.ExtraData = teleportTwo.Id.ToString();
										await session.Player.Inventory.UpdateItem(teleportOne);
										itemsList.Add(teleportOne);
										itemsList.Add(teleportTwo);
									}
									else
									{
										InventoryItem habboItem = new InventoryItem(-1, limitedNumber, limitedStack, baseItem.Id, session.Player.Id, extraData);
										await session.Player.Inventory.AddItem(habboItem);
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
				session.Player.Credits -= totalCredits;
				session.Send(new UserCreditsComposer(session.Player));
			}

			if (totalPoints > 0)
			{
				//session.Player.Currency.GetCurrencyType(item.PointsType).Amount -= totalPoints; todo:
				//session.Send(new UserPointsComposer(session.Player.Currency.GetCurrencyType(item.PointsType).Amount, -totalPoints, item.PointsType));
			}

			if (item.IsLimited)
			{
				item.AddLimited(limitedNumber);
				await Alias.Server.CatalogManager.AddLimitedAsync(item.Id, limitedNumber);
			}

			if (itemsList != null)
			{
				session.Send(new AddPlayerItemsComposer(itemsList));
			}

			session.Player.AddPurchase(item);

			session.Send(new PurchaseOKComposer(item));
			session.Send(new InventoryRefreshComposer());
		}
	}
}
