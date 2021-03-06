using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Trading.Composers
{
    class TradeAcceptedComposer : IPacketComposer
	{
		private TradeUser tradeUser;

		public TradeAcceptedComposer(TradeUser tradeUser)
		{
			this.tradeUser = tradeUser;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.TradeAcceptedMessageComposer);
			message.WriteInteger(this.tradeUser.User.Player.Id);
			message.WriteInteger(this.tradeUser.Accepted ? 1 : 0);
			return message;
		}
	}
}
