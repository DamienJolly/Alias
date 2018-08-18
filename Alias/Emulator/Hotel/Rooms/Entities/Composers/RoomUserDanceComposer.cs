using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Composers
{
	class RoomUserDanceComposer : IPacketComposer
	{
		private RoomEntity user;

		public RoomUserDanceComposer(RoomEntity user)
		{
			this.user = user;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomUserDanceMessageComposer);
			message.WriteInteger(this.user.VirtualId);
			message.WriteInteger(this.user.DanceId);
			return message;
		}
	}
}
