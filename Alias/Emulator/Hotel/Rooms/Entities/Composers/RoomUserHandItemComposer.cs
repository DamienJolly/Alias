using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Composers
{
	class RoomUserHandItemComposer : IPacketComposer
	{
		private RoomEntity user;

		public RoomUserHandItemComposer(RoomEntity user)
		{
			this.user = user;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomUserHandItemMessageComposer);
			message.WriteInteger(this.user.VirtualId);
			message.WriteInteger(this.user.HandItem);
			return message;
		}
	}
}
