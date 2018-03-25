using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomModelComposer : IPacketComposer
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
