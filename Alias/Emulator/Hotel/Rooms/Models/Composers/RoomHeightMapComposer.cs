using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Models.Composers
{
	public class RoomHeightMapComposer : IPacketComposer
	{
		private Room room;

		public RoomHeightMapComposer(Room r)
		{
			this.room = r;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomHeightMapMessageComposer);
			message.WriteBoolean(true);
			message.WriteInteger(-1); //todo: Wall height
			message.WriteString(this.room.Model.Map.Replace("\r\n", "\r").Substring(0, this.room.Model.Map.Replace("\r\n", "\r").Length));
			return message;
		}
	}
}
