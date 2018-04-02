using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Trading.Composers
{
    public class TradeClosedComposer : IPacketComposer
	{
		public static int USER_CANCEL_TRADE = 0;
		public static int ITEMS_NOT_FOUND = 1;

		private int userId;
		private int code;

		public TradeClosedComposer(int userId, int code)
		{
			this.userId = userId;
			this.code = code;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.TradeClosedMessageComposer);
			message.WriteInteger(this.userId);
			message.WriteInteger(this.code);
			return message;
		}
	}
}
