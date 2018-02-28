using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomModelComposer : IMessageComposer
	{
		private Room room;

		public RoomModelComposer(Room r)
		{
			this.room = r;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomModelMessageComposer);
			result.String(room.RoomData.ModelName);
			result.Int(room.Id);
			return result;
		}
	}
}
