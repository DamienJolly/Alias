using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	class RoomCreatedComposer : IPacketComposer
	{
		private Room room;

		public RoomCreatedComposer(Room room)
		{
			this.room = room;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomCreatedMessageComposer);
			message.WriteInteger(room.RoomData.Id);
			message.WriteString(room.RoomData.Name);
			return message;
		}
	}
}
