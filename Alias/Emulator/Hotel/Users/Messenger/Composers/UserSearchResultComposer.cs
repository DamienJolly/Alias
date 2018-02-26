using System.Collections.Generic;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class UserSearchResultComposer : MessageComposer
	{
		private List<Habbo> NotFriends;
		private List<Habbo> Friends;

		public UserSearchResultComposer(List<Habbo> habbos, Messenger messenger)
		{
			this.NotFriends = new List<Habbo>();
			this.Friends = new List<Habbo>();
			habbos.ForEach(hab =>
			{
				if (messenger.IsFriend(hab.Id))
				{
					this.Friends.Add(hab);
				}
				else
				{
					this.NotFriends.Add(hab);
				}
			});
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.UserSearchResultMessageComposer);
			result.Int(this.Friends.Count);
			this.Friends.ForEach(habbo =>
			{
				result.Int(habbo.Id);
				result.String(habbo.Username);
				result.String(habbo.Motto);
				result.Boolean(SessionManager.IsOnline(habbo.Id));
				result.Boolean(false);
				result.String("");
				result.Int(0);
				result.String(habbo.Look);
				result.String("01.01.1970 00:00:00"); //LastOnline
			});
			result.Int(this.NotFriends.Count);
			this.NotFriends.ForEach(habbo =>
			{
				result.Int(habbo.Id);
				result.String(habbo.Username);
				result.String(habbo.Motto);
				result.Boolean(SessionManager.IsOnline(habbo.Id));
				result.Boolean(false);
				result.String("");
				result.Int(0);
				result.String(habbo.Look);
				result.String("01.01.1970 00:00:00"); //LastOnline
			});
			return result;
		}
	}
}
