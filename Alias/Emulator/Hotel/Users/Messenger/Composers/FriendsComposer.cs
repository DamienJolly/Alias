using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class FriendsComposer : IPacketComposer
	{
		private List<MessengerFriend> FriendList;

		public FriendsComposer(List<MessengerFriend> friendList)
		{
			this.FriendList = friendList;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.FriendsMessageComposer);
			message.WriteInteger(1);
			message.WriteInteger(0);
			message.WriteInteger(this.FriendList.Count);
			this.FriendList.ForEach(friend =>
			{
				message.WriteInteger(friend.Id);
				message.WriteString(friend.Username);
				message.WriteInteger(1); //Gender???
				message.WriteBoolean(Alias.Server.SocketServer.SessionManager.IsOnline(friend.Id));
				message.WriteBoolean(false); //InRoom
				message.WriteString(friend.Look);
				message.WriteInteger(0); //category id
				message.WriteString(friend.Motto);
				message.WriteString("");
				message.WriteString("");
				message.WriteBoolean(true);
				message.WriteBoolean(false);
				message.WriteBoolean(false);
				message.WriteShort(friend.Relation);
			});
			return message;
		}
	}
}
