using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Composers
{
	class RoomUserDataComposer : IPacketComposer
	{
		private RoomEntity entity;

		public RoomUserDataComposer(RoomEntity entity)
		{
			this.entity = entity;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomUserDataMessageComposer);
			message.WriteInteger(this.entity.VirtualId);
			message.WriteString(this.entity.Look);
			message.WriteString(this.entity.Gender);
			message.WriteString(this.entity.Motto);
			message.WriteInteger(this.entity.Type == RoomEntityType.Player ? this.entity.Player.AchievementScore : 0);
			return message;
		}
	}
}
