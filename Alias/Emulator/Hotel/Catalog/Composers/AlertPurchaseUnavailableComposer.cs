using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class AlertPurchaseUnavailableComposer : IMessageComposer
	{
		public static int ILLEGAL = 0;
		public static int REQUIRES_CLUB = 1;

		int code;

		public AlertPurchaseUnavailableComposer(int code)
		{
			this.code = code;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.AlertPurchaseUnavailableMessageComposer);
			message.Int(this.code);
			return message;
		}
	}
}
