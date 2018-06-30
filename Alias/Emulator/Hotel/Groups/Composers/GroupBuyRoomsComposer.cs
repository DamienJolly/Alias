using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Groups.Composers
{
    class GroupBuyRoomsComposer : IPacketComposer
	{
		private List<RoomData> rooms;

		public GroupBuyRoomsComposer(List<RoomData> rooms)
		{
			this.rooms = rooms;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GroupBuyRoomsMessageComposer);
			message.WriteInteger(10); // price
			message.WriteInteger(this.rooms.Count);
			this.rooms.ForEach(room =>
			{
				message.WriteInteger(room.Id);
				message.WriteString(room.Name);
				message.WriteBoolean(false);
			});

			message.WriteInteger(5);

			message.WriteInteger(10);
			message.WriteInteger(3);
			message.WriteInteger(4);

			message.WriteInteger(25);
			message.WriteInteger(17);
			message.WriteInteger(5);

			message.WriteInteger(25);
			message.WriteInteger(17);
			message.WriteInteger(3);

			message.WriteInteger(29);
			message.WriteInteger(11);
			message.WriteInteger(4);

			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(0);
			return message;
		}
	}
}
