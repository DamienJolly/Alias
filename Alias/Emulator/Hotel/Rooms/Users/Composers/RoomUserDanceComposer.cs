using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Users.Composers
{
	public class RoomUserDanceComposer : MessageComposer
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
