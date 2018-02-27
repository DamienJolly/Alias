using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class UpdateFriendComposer : MessageComposer
	{
		private MessengerFriend Friend;
		private int FriendId = 0;

		public UpdateFriendComposer(MessengerFriend friend)
		{
			this.Friend = friend;
		}

		public UpdateFriendComposer(int friendId)
		{
			this.FriendId = friendId;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.UpdateFriendMessageComposer);
			if (this.FriendId > 0)
			{
				message.Int(0);
				message.Int(1);
				message.Int(-1);
				message.Int(this.FriendId);
			}
			else
			{
				message.Int(0);
				message.Int(1);
				message.Int(0);
				message.Int(this.Friend.Id);
				message.String(this.Friend.Username);
				message.Int(1); //Gender???
				message.Boolean(SessionManager.IsOnline(this.Friend.Id));
				message.Boolean(this.Friend.InRoom); //InRoom
				message.String(this.Friend.Look);
				message.Int(0); //category id
				message.String(this.Friend.Motto);
				message.String("");
				message.String("");
				message.Boolean(true);
				message.Boolean(false);
				message.Boolean(false);
				message.Short(0);//relation
			}
			return message;
		}
	}
}
