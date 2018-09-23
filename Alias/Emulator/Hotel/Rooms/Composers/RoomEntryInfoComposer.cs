using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	class RoomEntryInfoComposer : IPacketComposer
	{
		private Room room;
		private Player habbo;

		public RoomEntryInfoComposer(Room r, Player h)
		{
			this.room = r;
			this.habbo = h;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomEntryInfoMessageComposer);
			message.WriteInteger(this.room.Id);
			message.WriteBoolean(this.room.RoomRights.HasRights(this.habbo.Id));
			return message;
		}
	}
}
