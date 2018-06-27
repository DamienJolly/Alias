using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	class RoomModelComposer : IPacketComposer
	{
		private Room room;

		public RoomModelComposer(Room r)
		{
			this.room = r;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomModelMessageComposer);
			message.WriteString(room.RoomData.ModelName);
			message.WriteInteger(room.Id);
			return message;
		}
	}
}
