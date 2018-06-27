using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Rights.Composers
{
	class RoomRightsListComposer : IPacketComposer
	{
		Room Room;

		public RoomRightsListComposer(Room room)
		{
			this.Room = room;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomRightsListMessageComposer);
			message.WriteInteger(this.Room.Id);
			message.WriteInteger(this.Room.RoomRights.UserRights.Count);
			this.Room.RoomRights.UserRights.ForEach(right =>
			{
				message.WriteInteger(right.Id);
				message.WriteString(right.Username);
			});
			return message;
		}
	}
}
