using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Models.Composers
{
	class RoomRelativeMapComposer : IPacketComposer
	{
		private Room room;

		public RoomRelativeMapComposer(Room r)
		{
			this.room = r;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomRelativeMapMessageComposer);
			string[] split = this.room.Model.Map.Replace("\n", "").Split('\r');

			message.WriteInteger(split[0].Length);
			message.WriteInteger((split.Length) * split[0].Length);
			for (int y = 0; y < split.Length; y++)
			{
				for (int x = 0; x < split[0].Length; x++)
				{
					char position = split[y][x];
					if (position == 'x')
					{
						message.WriteShort(-1);
					}
					else
					{
						message.WriteShort((int)Alias.ParseChar(position) * 256);
					}
				}
			}
			return message;
		}
	}
}
