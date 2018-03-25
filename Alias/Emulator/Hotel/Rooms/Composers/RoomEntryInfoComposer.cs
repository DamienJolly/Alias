using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomEntryInfoComposer : IPacketComposer
	{
		private Room room;
		private Habbo habbo;

		public RoomEntryInfoComposer(Room r, Habbo h)
		{
			this.room = r;
			this.habbo = h;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomEntryInfoMessageComposer);
			result.Int(this.room.Id);
			result.Boolean(this.room.RoomRights.HasRights(this.habbo.Id));
			return result;
		}
	}
}
