using System.Collections.Generic;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class FriendsComposer : IMessageComposer
	{
		private List<MessengerFriend> FriendList;

		public FriendsComposer(List<MessengerFriend> friendList)
		{
			this.FriendList = friendList;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.FriendsMessageComposer);
			message.Int(1);
			message.Int(0);
			message.Int(this.FriendList.Count);
			this.FriendList.ForEach(friend =>
			{
				message.Int(friend.Id);
				message.String(friend.Username);
				message.Int(1); //Gender???
				message.Boolean(SessionManager.IsOnline(friend.Id));
				message.Boolean(false); //InRoom
				message.String(friend.Look);
				message.Int(0); //category id
				message.String(friend.Motto);
				message.String("");
				message.String("");
				message.Boolean(true);
				message.Boolean(false);
				message.Boolean(false);
				message.Short(friend.Relation);
			});
			return message;
		}
	}
}
