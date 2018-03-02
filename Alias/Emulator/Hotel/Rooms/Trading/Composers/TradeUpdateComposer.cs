using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Trading.Composers
{
    public class TradeUpdateComposer : IMessageComposer
	{
		private RoomTrade roomTrade;

		public TradeUpdateComposer(RoomTrade roomTrade)
		{
			this.roomTrade = roomTrade;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.TradeUpdateMessageComposer);
			roomTrade.Users.ForEach(user =>
			{
				result.Int(user.User.Habbo.Id);
				result.Int(user.OfferedItems.Count);
				user.OfferedItems.ForEach(item =>
				{
					result.Int(item.Id);
					result.String(item.ItemData.Type.ToUpper());
					result.Int(item.Id);
					result.Int(item.ItemData.Id);
					result.Int(0);
					result.Boolean(true); //can stack
					result.Int(0); //todo: extradata
					result.String("");
					result.Int(0); //rent shit v
					result.Int(0);
					result.Int(0);

					if (item.ItemData.Type.ToUpper() == "S")
					{
						result.Int(0);
					}
				});
				result.Int(user.OfferedItems.Count);
				result.Int(0); // todo: exchange items value
			});
			return result;
		}
	}
}
