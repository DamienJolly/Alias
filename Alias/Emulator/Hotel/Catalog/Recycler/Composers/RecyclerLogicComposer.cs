using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Recycler.Composers
{
    class RecyclerLogicComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			//todo: recycler prizes
			ServerPacket message = new ServerPacket(Outgoing.RecyclerLogicMessageComposer);
			message.WriteInteger(0);
			return message;
		}
	}
}
