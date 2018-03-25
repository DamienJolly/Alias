using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Trading.Composers
{
    public class TradeStartComposer : IPacketComposer
	{
		private RoomTrade roomTrade;

		public TradeStartComposer(RoomTrade roomTrade)
		{
			this.roomTrade = roomTrade;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.TradeStartMessageComposer);
			roomTrade.Users.ForEach(user =>
			{
				result.Int(user.User.Habbo.Id);
				result.Int(1);
			});
			return result;
		}
	}
}
