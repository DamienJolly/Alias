using System;
using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Users.Messenger;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class ProfileFriendsComposer : IPacketComposer
	{
		private int userId;
		private Random rand;
		private List<MessengerFriend> love;
		private List<MessengerFriend> happy;
		private List<MessengerFriend> sad;

		public ProfileFriendsComposer(int userId, List<MessengerFriend> friends)
		{
			this.rand = new Random();
			this.userId = userId;
			this.love = friends.Where(friend => friend.Relation == 1).ToList();
			this.happy = friends.Where(friend => friend.Relation == 2).ToList();
			this.sad = friends.Where(friend => friend.Relation == 3).ToList();
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.ProfileFriendsMessageComposer);
			message.WriteInteger(this.userId);

			int total = 0;

			if (this.love.Count > 0)
				total++;
			if (this.happy.Count > 0)
				total++;
			if (this.sad.Count > 0)
				total++;

			message.WriteInteger(total);

			if (this.love.Count > 0)
			{
				MessengerFriend friend = this.love[rand.Next(this.love.Count)];
				message.WriteInteger(1);
				message.WriteInteger(love.Count);
				message.WriteInteger(friend.Id);
				message.WriteString(friend.Username);
				message.WriteString(friend.Look);
			}
			if (this.happy.Count > 0)
			{
				MessengerFriend friend = this.happy[rand.Next(this.happy.Count)];
				message.WriteInteger(2);
				message.WriteInteger(happy.Count);
				message.WriteInteger(friend.Id);
				message.WriteString(friend.Username);
				message.WriteString(friend.Look);
			}
			if (this.sad.Count > 0)
			{
				MessengerFriend friend = this.sad[rand.Next(this.sad.Count)];
				message.WriteInteger(3);
				message.WriteInteger(love.Count);
				message.WriteInteger(friend.Id);
				message.WriteString(friend.Username);
				message.WriteString(friend.Look);
			}
			return message;
		}
	}
}
