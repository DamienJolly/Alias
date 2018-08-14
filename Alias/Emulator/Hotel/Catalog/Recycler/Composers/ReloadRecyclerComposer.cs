using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Recycler.Composers
{
    class ReloadRecyclerComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.ReloadRecyclerMessageComposer);
			message.WriteInteger(1);
			message.WriteInteger(0);
			return message;
		}
	}
}
