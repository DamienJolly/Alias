using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	class RoomThicknessComposer : IPacketComposer
	{
		private Room room;

		public RoomThicknessComposer(Room r)
		{
			this.room = r;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomThicknessMessageComposer);
			message.WriteBoolean(this.room.RoomData.Settings.HideWalls);
			message.WriteInteger(this.room.RoomData.Settings.FloorSize);
			message.WriteInteger(this.room.RoomData.Settings.WallHeight);
			return message;
		}
	}
}
