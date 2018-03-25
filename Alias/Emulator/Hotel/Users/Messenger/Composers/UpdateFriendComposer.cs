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

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.UpdateFriendMessageComposer);
			if (this.friendId > 0)
			{
				message.Int(0);
				message.Int(1);
				message.Int(-1);
				message.Int(this.friendId);
			}
			else
			{
				message.Int(0);
				message.Int(1);
				message.Int(0);
				message.Int(this.friend.Id);
				message.String(this.friend.Username);
				message.Int(1); //Gender???
				message.Boolean(SessionManager.IsOnline(this.friend.Id));
				message.Boolean(this.friend.InRoom);
				message.String(this.friend.Look);
				message.Int(0); //category id
				message.String(this.friend.Motto);
				message.String("");
				message.String("");
				message.Boolean(true);
				message.Boolean(false);
				message.Boolean(false);
				message.Short(this.friend.Relation);
			}
			return message;
		}
	}
}
