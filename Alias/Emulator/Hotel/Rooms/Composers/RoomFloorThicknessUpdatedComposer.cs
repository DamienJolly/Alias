using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomFloorThicknessUpdatedComposer : IMessageComposer
	{
		private Room room;

		public RoomFloorThicknessUpdatedComposer(Room r)
		{
			this.room = r;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomFloorThicknessUpdatedMessageComposer);
			result.Boolean(false); //todo:
			result.Int(1);
			result.Int(1);
			return result;
		}
	}
}
