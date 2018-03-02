using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Rooms.Trading.Composers;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Hotel.Rooms.Users.Composers;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Network.Messages;

namespace Alias.Emulator.Hotel.Rooms.Trading
{
    public class RoomTrade
    {
		public List<TradeUser> Users
		{
			get; set;
		}

		public void OfferItems(RoomUser user, List<InventoryItem> items)
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

		public void RemoveItem(RoomUser user, InventoryItem item)
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

		public void HandleItems()
		{
			this.Users.ForEach(tradeUser =>
			{
				tradeUser.OfferedItems.ForEach(tradeItem =>
				{
					if (tradeUser.User.Habbo.Inventory.GetFloorItem(tradeItem.Id) == null)
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
				RoomTradingDatabase.LogTradeItem(logId, userOne.User.Habbo.Id, item.Id);
				userOne.User.Habbo.Inventory.RemoveItem(item);
				userTwo.User.Habbo.Inventory.AddItems(new List<InventoryItem>() { item });
			}

			foreach (InventoryItem item in userTwo.OfferedItems)
			{
				RoomTradingDatabase.LogTradeItem(logId, userTwo.User.Habbo.Id, item.Id);
				userTwo.User.Habbo.Inventory.RemoveItem(item);
				userOne.User.Habbo.Inventory.AddItems(new List<InventoryItem>() { item });
			}

			userOne.User.Habbo.Session.Send(new AddHabboItemsComposer(userTwo.OfferedItems));
			userTwo.User.Habbo.Session.Send(new AddHabboItemsComposer(userOne.OfferedItems));

			this.Send(new InventoryRefreshComposer());
		}

		public void Confirm(RoomUser user)
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

		public void StopTrade(RoomUser user)
		{
			this.Users.ForEach(tradeUser =>
			{
				if (tradeUser.User != user)
				{
					tradeUser.User.Habbo.Session.Send(new TradeClosedComposer(user.VirtualId, TradeClosedComposer.USER_CANCEL_TRADE));
				}
			});
			user.Room.RoomTrading.EndTrade(this);
			this.RemoveStatusses();
		}

		private void RemoveStatusses()
		{
			this.Users.ForEach(tradeUser =>
			{
				RoomUser user = tradeUser.User;
				if (user != null)
				{
					user.Actions.Remove("trd");
					user.Room.UserManager.Send(new RoomUserStatusComposer(user));
				}
			});
		}

		public void Accept(RoomUser user, bool value)
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

		public TradeUser GetTradeUser(RoomUser user)
		{
			return this.Users.Where(usr => usr.User == user).FirstOrDefault();
		}

		public void Send(IMessageComposer message)
		{
			this.Users.ForEach(user =>
			{
				user.User.Habbo.Session.Send(message);
			});
		}
	}
}
