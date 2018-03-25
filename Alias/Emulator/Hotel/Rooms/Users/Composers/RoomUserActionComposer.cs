using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Users.Composers
{
	public class RoomUserActionComposer : IPacketComposer
	{
		private RoomUser user;
		private int actionId;

		public RoomUserActionComposer(RoomUser user, int actionId)
		{
			this.user = user;
			this.actionId = actionId;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomUserActionMessageComposer);
			result.Int(this.user.VirtualId);
			result.Int(this.actionId);
			return result;
		}
	}
}
