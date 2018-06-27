using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Rooms.Trading.Composers;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Hotel.Rooms.Users.Composers;

namespace Alias.Emulator.Hotel.Rooms.Trading
{
    class RoomTrading
    {
		private Room Room;

		public List<RoomTrade> Trades
		{
			get; set;
		}

		public RoomTrading(Room room)
		{
			this.Trades = new List<RoomTrade>();
			this.Room = room;
		}

		public RoomTrade GetActiveTrade(RoomUser user)
		{
			return this.Trades.Where(trade => trade.Users.Where(usr => usr.User == user).Count() == 1).FirstOrDefault();
		}

		public void StartTrade(RoomUser userOne, RoomUser userTwo)
		{
			List<TradeUser> users = new List<TradeUser>();
			users.Add(new TradeUser() { User = userOne });
			users.Add(new TradeUser() { User = userTwo });

			RoomTrade trade = new RoomTrade()
			{
				Users = users
			};

			this.Trades.Add(trade);

			users.ForEach(user =>
			{
				if (!user.User.Actions.Has("trd"))
				{
					user.User.Actions.Add("trd", "");
					this.Room.UserManager.Send(new RoomUserStatusComposer(user.User));
				}
			});
			
			trade.Send(new TradeStartComposer(trade));
		}

		public void EndTrade(RoomTrade trade)
		{
			this.Trades.Remove(trade);
		}
	}
}
