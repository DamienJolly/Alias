using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Users.Composers
{
	class RoomUserEffectComposer : IPacketComposer
	{
		private RoomUser user;

		public RoomUserEffectComposer(RoomUser user)
		{
			this.user = user;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomUserEffectMessageComposer);
			message.WriteInteger(this.user.VirtualId);
			message.WriteInteger(this.user.EffectId);
			message.WriteInteger(0);
			return message;
		}
	}
}
