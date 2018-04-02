using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class CatalogModeComposer : IPacketComposer
	{
		private int mode;

		public CatalogModeComposer(int mode)
		{
			this.mode = mode;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.CatalogModeMessageComposer);
			message.WriteInteger(this.mode);
			return message;
		}
	}
}
