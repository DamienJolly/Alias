using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Models.Composers
{
    public class FloorPlanEditorBlockedTilesComposer : IPacketComposer
	{
		private Room room;

		public FloorPlanEditorBlockedTilesComposer(Room r)
		{
			this.room = r;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.FloorPlanEditorBlockedTilesMessageComposer);
			message.WriteInteger(0); // count
			{
				//message.WriteInteger(0); // x
				//message.WriteInteger(0); // y
			}
			return message;
		}
	}
}
