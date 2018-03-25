using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomOpenComposer : IPacketComposer
	{
		public ServerMessage Compose()
		{
			return new ServerMessage(Outgoing.RoomOpenMessageComposer);
		}
	}
}
