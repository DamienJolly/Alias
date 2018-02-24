using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class UserHomeRoomComposer : MessageComposer
	{
		private int HomeRoomId = 0;

		public UserHomeRoomComposer(int homeRoom)
		{
			this.HomeRoomId = homeRoom;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.UserHomeRoomMessageComposer);
			message.Int(this.HomeRoomId);
			message.Int(this.HomeRoomId);
			return message;
		}
	}
}
