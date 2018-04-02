using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class AlertPurchaseFailedComposer : IPacketComposer
	{
		public static int SERVER_ERROR = 0;
		public static int ALREADY_HAVE_BADGE = 1;

		private int error;

		public AlertPurchaseFailedComposer(int error)
		{
			this.error = error;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.AlertPurchaseFailedMessageComposer);
			message.WriteInteger(this.error);
			return message;
		}
	}
}
