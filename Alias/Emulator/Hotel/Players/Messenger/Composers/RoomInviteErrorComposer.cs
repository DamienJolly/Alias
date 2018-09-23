using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Messenger.Composers
{
	public class RoomInviteErrorComposer : IPacketComposer
	{
		public static readonly int FRIEND_MUTED = 3;
		public static readonly int YOU_ARE_MUTED = 4;
		public static readonly int FRIEND_NOT_ONLINE = 5; //Offline Messages?
		public static readonly int NO_FRIENDS = 6;
		public static readonly int FRIEND_BUSY = 7;
		public static readonly int OFFLINE_FAILED = 10;

		private int _errorCode;
		private int _target;

		public RoomInviteErrorComposer(int error, int target)
		{
			_errorCode = error;
			_target = target;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomInviteErrorMessageComposer);
			message.WriteInteger(_errorCode);
			message.WriteInteger(_target);
			message.WriteString("");
			return message;
		}
	}
}
