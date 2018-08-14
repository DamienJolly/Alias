using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Composers
{
	class RoomUserDataComposer : IPacketComposer
	{
		private Habbo habbo;

		public RoomUserDataComposer(Habbo habbo)
		{
			this.habbo = habbo;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomUserDataMessageComposer);
			message.WriteInteger(this.habbo.CurrentRoom.EntityManager == null ? -1 : this.habbo.CurrentRoom.EntityManager.UserByUserid(habbo.Id).VirtualId);
			message.WriteString(this.habbo.Look);
			message.WriteString(this.habbo.Gender);
			message.WriteString(this.habbo.Motto);
			message.WriteInteger(this.habbo.AchievementScore);
			return message;
		}
	}
}
