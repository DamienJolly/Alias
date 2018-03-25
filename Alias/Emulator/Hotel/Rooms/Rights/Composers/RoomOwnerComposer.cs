using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Rights.Composers
{
	public class RoomOwnerComposer : IPacketComposer
	{
		public ServerMessage Compose()
		{
			return new ServerMessage(Outgoing.RoomOwnerMessageComposer);
		}
	}
}
