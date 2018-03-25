using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class FriendFindingRoomComposer : IPacketComposer
	{
		public static int NO_ROOM_FOUND = 0;
		public static int ROOM_FOUND = 1;

		private int errorCode;

		public FriendFindingRoomComposer(int errorCode)
		{
			this.errorCode = errorCode;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.FriendFindingRoomMessageComposer);
			message.Int(this.errorCode);
			return message;
		}
	}
}
