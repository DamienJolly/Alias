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

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.CatalogModeMessageComposer);
			result.Int(this.mode);
			return result;
		}
	}
}
