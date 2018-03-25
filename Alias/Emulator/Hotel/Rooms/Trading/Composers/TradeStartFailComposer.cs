using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Trading.Composers
{
    public class TradeStartFailComposer : IPacketComposer
	{
		public static int HOTEL_TRADING_NOT_ALLOWED = 1;
		public static int YOU_TRADING_OFF = 2;
		public static int TARGET_TRADING_OFF = 4;
		public static int ROOM_TRADING_NOT_ALLOWED = 6;
		public static int YOU_ALREADY_TRADING = 7;
		public static int TARGET_ALREADY_TRADING = 8;

		private int code;
		private string username;

		public TradeStartFailComposer(int code, string username = "")
		{
			this.code = code;
			this.username = username;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.TradeStartFailMessageComposer);
			result.Int(this.code);
			result.String(this.username);
			return result;
		}
	}
}
