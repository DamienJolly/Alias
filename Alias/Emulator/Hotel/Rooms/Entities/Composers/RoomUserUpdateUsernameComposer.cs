using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Composers
{
	class RoomUserUpdateUsernameComposer : IPacketComposer
	{
		private RoomEntity entity;

		public RoomUserUpdateUsernameComposer(RoomEntity entity)
		{
			this.entity = entity;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomUserUpdateUsernameMessageComposer);
			message.WriteInteger(this.entity.Id);
			message.WriteInteger(this.entity.Id);
			message.WriteString(this.entity.Name);
			return message;
		}
	}
}
