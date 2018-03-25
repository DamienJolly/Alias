using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class RoomInviteErrorComposer : IPacketComposer
	{
		public static readonly int FRIEND_MUTED = 3;
		public static readonly int YOU_ARE_MUTED = 4;
		public static readonly int FRIEND_NOT_ONLINE = 5; //Offline Messages?
		public static readonly int NO_FRIENDS = 6;
		public static readonly int FRIEND_BUSY = 7;
		public static readonly int OFFLINE_FAILED = 10;

		private int ErrorCode;
		private int Target;

		public RoomInviteErrorComposer(int error, int target)
		{
			this.ErrorCode = error;
			this.Target = target;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.RoomInviteErrorMessageComposer);
			message.Int(ErrorCode);
			message.Int(Target);
			message.String("");
			return message;
		}
	}
}
