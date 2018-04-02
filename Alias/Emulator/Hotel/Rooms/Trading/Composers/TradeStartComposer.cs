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

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.TradeStartMessageComposer);
			roomTrade.Users.ForEach(user =>
			{
				message.WriteInteger(user.User.Habbo.Id);
				message.WriteInteger(1);
			});
			return message;
		}
	}
}
