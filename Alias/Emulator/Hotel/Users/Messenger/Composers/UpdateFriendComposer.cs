using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class UpdateFriendComposer : IPacketComposer
	{
		private MessengerFriend friend;
		private int friendId = 0;

		public UpdateFriendComposer(MessengerFriend friend)
		{
			this.friend = friend;
		}

		public UpdateFriendComposer(int friendId)
		{
			this.friendId = friendId;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UpdateFriendMessageComposer);
			if (this.friendId > 0)
			{
				message.WriteInteger(0);
				message.WriteInteger(1);
				message.WriteInteger(-1);
				message.WriteInteger(this.friendId);
			}
			else
			{
				message.WriteInteger(0);
				message.WriteInteger(1);
				message.WriteInteger(0);
				message.WriteInteger(this.friend.Id);
				message.WriteString(this.friend.Username);
				message.WriteInteger(1); //Gender???
				message.WriteBoolean(Alias.Server.SocketServer.SessionManager.IsOnline(this.friend.Id));
				message.WriteBoolean(this.friend.InRoom);
				message.WriteString(this.friend.Look);
				message.WriteInteger(0); //category id
				message.WriteString(this.friend.Motto);
				message.WriteString("");
				message.WriteString("");
				message.WriteBoolean(true);
				message.WriteBoolean(false);
				message.WriteBoolean(false);
				message.WriteShort(this.friend.Relation);
			}
			return message;
		}
	}
}
