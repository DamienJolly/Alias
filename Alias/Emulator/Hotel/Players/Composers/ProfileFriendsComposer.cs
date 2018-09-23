using System;
using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Players.Messenger;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Composers
{
	public class ProfileFriendsComposer : IPacketComposer
	{
		private int _userId;
		private Random _rand;
		private List<MessengerFriend> _love;
		private List<MessengerFriend> _happy;
		private List<MessengerFriend> _sad;

		public ProfileFriendsComposer(int userId, ICollection<MessengerFriend> friends)
		{
			_rand = new Random();
			_userId = userId;
			_love = friends.Where(friend => friend.Relation == 1).ToList();
			_happy = friends.Where(friend => friend.Relation == 2).ToList();
			_sad = friends.Where(friend => friend.Relation == 3).ToList();
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.ProfileFriendsMessageComposer);
			message.WriteInteger(_userId);

			int total = 0;
			if (_love.Count > 0)
				total++;
			if (_happy.Count > 0)
				total++;
			if (_sad.Count > 0)
				total++;

			message.WriteInteger(total);
			if (_love.Count > 0)
			{
				MessengerFriend friend = _love[_rand.Next(_love.Count)];
				message.WriteInteger(1);
				message.WriteInteger(_love.Count);
				message.WriteInteger(friend.Id);
				message.WriteString(friend.Username);
				message.WriteString(friend.Look);
			}
			if (_happy.Count > 0)
			{
				MessengerFriend friend = _happy[_rand.Next(_happy.Count)];
				message.WriteInteger(2);
				message.WriteInteger(_happy.Count);
				message.WriteInteger(friend.Id);
				message.WriteString(friend.Username);
				message.WriteString(friend.Look);
			}
			if (_sad.Count > 0)
			{
				MessengerFriend friend = _sad[_rand.Next(_sad.Count)];
				message.WriteInteger(3);
				message.WriteInteger(_love.Count);
				message.WriteInteger(friend.Id);
				message.WriteString(friend.Username);
				message.WriteString(friend.Look);
			}
			return message;
		}
	}
}
