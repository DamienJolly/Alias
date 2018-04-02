using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class AlertPurchaseUnavailableComposer : IPacketComposer
	{
		public static int ILLEGAL = 0;
		public static int REQUIRES_CLUB = 1;

		int code;

		public AlertPurchaseUnavailableComposer(int code)
		{
			this.code = code;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.AlertPurchaseUnavailableMessageComposer);
			message.WriteInteger(this.code);
			return message;
		}
	}
}
