using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Messenger.Composers
{
	class UserSearchResultComposer : IPacketComposer
	{
		private readonly List<Player> _notFriends;
		private readonly List<Player> _friends;

		public UserSearchResultComposer(List<Player> players, MessengerComponent messenger)
		{
			_notFriends = new List<Player>();
			_friends = new List<Player>();
			players.ForEach(player =>
			{
				if (messenger.Friends.ContainsKey(player.Id))
				{
					_friends.Add(player);
				}
				else
				{
					_notFriends.Add(player);
				}
			});
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserSearchResultMessageComposer);
			message.WriteInteger(_friends.Count);
			_friends.ForEach(player =>
			{
				message.WriteInteger(player.Id);
				message.WriteString(player.Username);
				message.WriteString(player.Motto);
				message.WriteBoolean(Alias.Server.PlayerManager.IsOnline(player.Id));
				message.WriteBoolean(false);
				message.WriteString("");
				message.WriteInteger(0);
				message.WriteString(player.Look);
				message.WriteString("01.01.1970 00:00:00"); //LastOnline
			});
			message.WriteInteger(_friends.Count);
			_friends.ForEach(player =>
			{
				message.WriteInteger(player.Id);
				message.WriteString(player.Username);
				message.WriteString(player.Motto);
				message.WriteBoolean(Alias.Server.PlayerManager.IsOnline(player.Id));
				message.WriteBoolean(false);
				message.WriteString("");
				message.WriteInteger(0);
				message.WriteString(player.Look);
				message.WriteString("01.01.1970 00:00:00"); //LastOnline
			});
			return message;
		}
	}
}
