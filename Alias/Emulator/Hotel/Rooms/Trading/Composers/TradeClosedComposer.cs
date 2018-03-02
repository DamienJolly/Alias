using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Trading.Composers
{
    public class TradeClosedComposer : IMessageComposer
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

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.TradeClosedMessageComposer);
			result.Int(this.userId);
			result.Int(this.code);
			return result;
		}
	}
}
