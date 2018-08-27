using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Composers
{
	class RoomUserTypingComposer : IPacketComposer
	{
		private RoomEntity entity;
		private bool typing;

		public RoomUserTypingComposer(RoomEntity entity, bool typing)
		{
			this.entity = entity;
			this.typing = typing;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomUserTypingMessageComposer);
			message.WriteInteger(this.entity.Id);
			message.WriteInteger(this.typing ? 1 : 0);
			return message;
		}
	}
}
