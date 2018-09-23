using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Rooms.Trading.Composers;
using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Hotel.Rooms.Entities.Composers;
using Alias.Emulator.Hotel.Players.Inventory;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Hotel.Players.Inventory.Composers;

namespace Alias.Emulator.Hotel.Rooms.Trading
{
    class RoomTrade
    {
		public List<TradeUser> Users
		{
			get; set;
		}

		public void OfferItems(RoomEntity user, List<InventoryItem> items)
		{
			TradeUser usr = this.GetTradeUser(user);
			items.ForEach(item =>
			{
				if (usr.OfferedItems.Contains(item))
				{
					return;
				}

				usr.OfferedItems.Add(item);
			});
			
			this.ClearAccept();
			this.Send(new TradeUpdateComposer(this));
		}

		public void RemoveItem(RoomEntity user, InventoryItem item)
		{
			TradeUser usr = this.GetTradeUser(user);
			if (!usr.OfferedItems.Contains(item))
			{
				return;
			}

			usr.OfferedItems.Remove(item);
			this.ClearAccept();
			this.Send(new TradeUpdateComposer(this));
		}

		public async void HandleItems()
		{
			this.Users.ForEach(tradeUser =>
			{
				tradeUser.OfferedItems.ForEach(tradeItem =>
				{
					if (!tradeUser.User.Player.Inventory.Items.ContainsKey(tradeItem.Id))
					{
						this.Send(new TradeClosedComposer(tradeUser.User.VirtualId, TradeClosedComposer.ITEMS_NOT_FOUND));
						return;
					}
				});
			});

			TradeUser userOne = this.Users[0];
			TradeUser userTwo = this.Users[1];

			int logId = RoomTradingDatabase.CreateTradeLog(userOne, userTwo);
			foreach (InventoryItem item in userOne.OfferedItems)
			{
				RoomTradingDatabase.LogTradeItem(logId, userOne.User.Player.Id, item.Id);
				item.UserId = userTwo.User.Player.Id;
				userOne.User.Player.Inventory.Items.Remove(item.Id);
				await userTwo.User.Player.Inventory.UpdateItem(item);
			}

			foreach (InventoryItem item in userTwo.OfferedItems)
			{
				RoomTradingDatabase.LogTradeItem(logId, userTwo.User.Player.Id, item.Id);
				item.UserId = userOne.User.Player.Id;
				userTwo.User.Player.Inventory.Items.Remove(item.Id);
				await userOne.User.Player.Inventory.UpdateItem(item);
			}

			userOne.User.Player.Session.Send(new AddPlayerItemsComposer(userTwo.OfferedItems));
			userTwo.User.Player.Session.Send(new AddPlayerItemsComposer(userOne.OfferedItems));

			this.Send(new InventoryRefreshComposer());
		}

		public void Confirm(RoomEntity user)
		{
			TradeUser usr = this.GetTradeUser(user);
			usr.Confirmed = true;
			this.Send(new TradeAcceptedComposer(usr));
			if (this.Users.Where(tradeUser => tradeUser.Confirmed).Count() == 2)
			{
				this.Send(new TradeCompleteComposer());
				this.HandleItems();
				usr.User.Room.RoomTrading.EndTrade(this);
				this.RemoveStatusses();
			}
		}

		public void StopTrade(RoomEntity user)
		{
			this.Users.ForEach(tradeUser =>
			{
				if (tradeUser.User != user)
				{
					tradeUser.User.Player.Session.Send(new TradeClosedComposer(user.VirtualId, TradeClosedComposer.USER_CANCEL_TRADE));
				}
			});
			user.Room.RoomTrading.EndTrade(this);
			this.RemoveStatusses();
		}

		private void RemoveStatusses()
		{
			this.Users.ForEach(tradeUser =>
			{
				RoomEntity user = tradeUser.User;
				if (user != null)
				{
					user.Actions.Remove("trd");
					user.Room.EntityManager.Send(new RoomUserStatusComposer(user));
				}
			});
		}

		public void Accept(RoomEntity user, bool value)
		{
			TradeUser usr = this.GetTradeUser(user);
			usr.Accepted = value;
			this.Send(new TradeAcceptedComposer(usr));
			if (this.Users.Where(tradeUser => tradeUser.Accepted).Count() == 2)
			{
				this.Send(new TradingWaitingConfirmComposer());
			}
		}

		public void ClearAccept()
		{
			this.Users.ForEach(user =>
			{
				user.Accepted = false;
			});
		}

		public TradeUser GetTradeUser(RoomEntity user)
		{
			return this.Users.Where(usr => usr.User == user).FirstOrDefault();
		}

		public void Send(IPacketComposer message)
		{
			this.Users.ForEach(user =>
			{
				user.User.Player.Session.Send(message);
			});
		}
	}
}
