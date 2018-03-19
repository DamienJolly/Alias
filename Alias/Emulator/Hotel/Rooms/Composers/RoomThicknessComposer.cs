using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomThicknessComposer : IMessageComposer
	{
		private Room room;

		public RoomThicknessComposer(Room r)
		{
			this.room = r;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomThicknessMessageComposer);
			result.Boolean(this.room.RoomData.Settings.HideWalls);
			result.Int(this.room.RoomData.Settings.FloorSize);
			result.Int(this.room.RoomData.Settings.WallHeight);
			return result;
		}
	}
}
