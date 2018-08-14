using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Composers
{
	class RoomUserDanceComposer : IPacketComposer
	{
		private RoomEntity user;
		private int danceId;

		public RoomUserDanceComposer(RoomEntity user, int danceId)
		{
			this.user = user;
			this.danceId = danceId;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomUserDanceMessageComposer);
			message.WriteInteger(this.user.VirtualId);
			message.WriteInteger(this.danceId);
			return message;
		}
	}
}
