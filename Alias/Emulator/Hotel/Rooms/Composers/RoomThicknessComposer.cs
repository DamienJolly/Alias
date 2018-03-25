using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomThicknessComposer : IPacketComposer
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
