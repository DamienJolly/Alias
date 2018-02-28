using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Models.Composers
{
	public class RoomHeightMapComposer : IMessageComposer
	{
		private Room room;

		public RoomHeightMapComposer(Room r)
		{
			this.room = r;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomHeightMapMessageComposer);
			result.Boolean(true);
			result.Int(-1); //todo: Wall height
			result.String(this.room.Model.Map.Replace("\r\n", "\r").Substring(0, this.room.Model.Map.Replace("\r\n", "\r").Length));
			return result;
		}
	}
}
