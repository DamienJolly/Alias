using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Promotions.Composers
{
    class PromoteOwnRoomsListComposer : IPacketComposer
	{
		private List<RoomData> rooms;

		public PromoteOwnRoomsListComposer(List<RoomData> rooms)
		{
			this.rooms = rooms;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.PromoteOwnRoomsListMessageComposer);
			message.WriteBoolean(true);
			message.WriteInteger(this.rooms.Count);
			this.rooms.ForEach(room =>
			{
				message.WriteInteger(room.Id);
				message.WriteString(room.Name);
				message.WriteBoolean(false); // ??
			});
			return message;
		}
	}
}
