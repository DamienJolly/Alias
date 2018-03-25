using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Users.Composers
{
	public class RoomUserDanceComposer : IPacketComposer
	{
		private RoomUser user;
		private int danceId;

		public RoomUserDanceComposer(RoomUser user, int danceId)
		{
			this.user = user;
			this.danceId = danceId;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomUserDanceMessageComposer);
			result.Int(this.user.VirtualId);
			result.Int(this.danceId);
			return result;
		}
	}
}
