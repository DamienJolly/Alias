using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class AlertLimitedSoldOutComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			return new ServerPacket(Outgoing.AlertLimitedSoldOutMessageComposer);
		}
	}
}
