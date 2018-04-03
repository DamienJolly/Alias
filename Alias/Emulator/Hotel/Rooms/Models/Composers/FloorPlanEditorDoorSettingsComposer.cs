using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Models.Composers
{
    public class FloorPlanEditorDoorSettingsComposer : IPacketComposer
	{
		private Room room;

		public FloorPlanEditorDoorSettingsComposer(Room r)
		{
			this.room = r;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.FloorPlanEditorDoorSettingsMessageComposer);
			message.WriteInteger(this.room.Model.Door.X);
			message.WriteInteger(this.room.Model.Door.Y);
			message.WriteInteger(this.room.Model.Door.Rotation);
			return message;
		}
	}
}
