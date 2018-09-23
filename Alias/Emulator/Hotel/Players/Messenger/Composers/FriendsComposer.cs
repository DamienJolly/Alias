using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Messenger.Composers
{
	public class FriendsComposer : IPacketComposer
	{
		private readonly ICollection<MessengerFriend> _friendList;

		public FriendsComposer(ICollection<MessengerFriend> friendList)
		{
			_friendList = friendList;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.FriendsMessageComposer);
			message.WriteInteger(1);
			message.WriteInteger(0);
			message.WriteInteger(_friendList.Count);
			foreach (MessengerFriend friend in _friendList)
			{
				message.WriteInteger(friend.Id);
				message.WriteString(friend.Username);
				message.WriteInteger(1); //Gender???
				message.WriteBoolean(Alias.Server.PlayerManager.IsOnline(friend.Id));
				message.WriteBoolean(friend.InRoom);
				message.WriteString(friend.Look);
				message.WriteInteger(0); //category id
				message.WriteString(friend.Motto);
				message.WriteString("");
				message.WriteString("");
				message.WriteBoolean(true);
				message.WriteBoolean(false);
				message.WriteBoolean(false);
				message.WriteShort(friend.Relation);
			}
			return message;
		}
	}
}
