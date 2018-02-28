using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Rights.Composers
{
	public class RoomRightsListComposer : IMessageComposer
	{
		Room Room;

		public RoomRightsListComposer(Room room)
		{
			this.Room = room;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomRightsListMessageComposer);
			result.Int(this.Room.Id);
			result.Int(this.Room.RoomRights.UserRights.Count);
			this.Room.RoomRights.UserRights.ForEach(right =>
			{
				result.Int(right.Id);
				result.String(right.Username);
			});
			return result;
		}
	}
}
