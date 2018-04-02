using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class UserSearchResultComposer : IPacketComposer
	{
		private List<Habbo> NotFriends;
		private List<Habbo> Friends;

		public UserSearchResultComposer(List<Habbo> habbos, MessengerComponent messenger)
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

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserSearchResultMessageComposer);
			message.WriteInteger(this.Friends.Count);
			this.Friends.ForEach(habbo =>
			{
				message.WriteInteger(habbo.Id);
				message.WriteString(habbo.Username);
				message.WriteString(habbo.Motto);
				message.WriteBoolean(Alias.Server.SocketServer.SessionManager.IsOnline(habbo.Id));
				message.WriteBoolean(false);
				message.WriteString("");
				message.WriteInteger(0);
				message.WriteString(habbo.Look);
				message.WriteString("01.01.1970 00:00:00"); //LastOnline
			});
			message.WriteInteger(this.NotFriends.Count);
			this.NotFriends.ForEach(habbo =>
			{
				message.WriteInteger(habbo.Id);
				message.WriteString(habbo.Username);
				message.WriteString(habbo.Motto);
				message.WriteBoolean(Alias.Server.SocketServer.SessionManager.IsOnline(habbo.Id));
				message.WriteBoolean(false);
				message.WriteString("");
				message.WriteInteger(0);
				message.WriteString(habbo.Look);
				message.WriteString("01.01.1970 00:00:00"); //LastOnline
			});
			return message;
		}
	}
}
