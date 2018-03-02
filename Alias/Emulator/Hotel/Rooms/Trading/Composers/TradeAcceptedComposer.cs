using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Trading.Composers
{
    public class TradeAcceptedComposer : IMessageComposer
	{
		private TradeUser tradeUser;

		public TradeAcceptedComposer(TradeUser tradeUser)
		{
			this.tradeUser = tradeUser;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.TradeAcceptedMessageComposer);
			result.Int(this.tradeUser.User.Habbo.Id);
			result.Int(this.tradeUser.Accepted ? 1 : 0);
			return result;
		}
	}
}
