using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Composers
{
	class RoomUserActionComposer : IPacketComposer
	{
		private RoomEntity user;
		private int actionId;

		public RoomUserActionComposer(RoomEntity user, int actionId)
		{
			this.user = user;
			this.actionId = actionId;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomUserActionMessageComposer);
			message.WriteInteger(this.user.VirtualId);
			message.WriteInteger(this.actionId);
			return message;
		}
	}
}
