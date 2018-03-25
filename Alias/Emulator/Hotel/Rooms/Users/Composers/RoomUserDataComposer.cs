using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Users.Composers
{
	public class RoomUserDataComposer : IPacketComposer
	{
		private Habbo habbo;

		public RoomUserDataComposer(Habbo habbo)
		{
			this.habbo = habbo;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomUserDataMessageComposer);
			result.Int(this.habbo.CurrentRoom.UserManager == null ? -1 : this.habbo.CurrentRoom.UserManager.UserByUserid(habbo.Id).VirtualId);
			result.String(this.habbo.Look);
			result.String(this.habbo.Gender);
			result.String(this.habbo.Motto);
			result.Int(this.habbo.AchievementScore);
			return result;
		}
	}
}
