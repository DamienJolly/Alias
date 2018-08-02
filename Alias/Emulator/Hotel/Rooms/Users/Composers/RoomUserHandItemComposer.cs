using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Users.Composers
{
	class RoomUserHandItemComposer : IPacketComposer
	{
		private RoomUser user;

		public RoomUserHandItemComposer(RoomUser user)
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
