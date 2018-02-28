using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class AlertPurchaseFailedComposer : IMessageComposer
	{
		public static int SERVER_ERROR = 0;
		public static int ALREADY_HAVE_BADGE = 1;

		private int error;

		public AlertPurchaseFailedComposer(int error)
		{
			this.error = error;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.AlertPurchaseFailedMessageComposer);
			result.Int(this.error);
			return result;
		}
	}
}
