using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Rights.Composers
{
	public class RoomRightsComposer : IPacketComposer
	{
		private int Id;

		public RoomRightsComposer(int id)
		{
			this.Id = id;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomRightsMessageComposer);
			message.WriteInteger(this.Id);
			return message;
		}
	}
}
