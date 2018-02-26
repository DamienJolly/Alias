using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomEntryInfoComposer : MessageComposer
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
			result.Boolean(true); //todo: is owner
			return result;
		}
	}
}
