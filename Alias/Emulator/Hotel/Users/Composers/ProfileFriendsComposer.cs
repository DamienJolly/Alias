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

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.ProfileFriendsMessageComposer);
			result.Int(this.userId);

			int total = 0;

			if (this.love.Count > 0)
				total++;
			if (this.happy.Count > 0)
				total++;
			if (this.sad.Count > 0)
				total++;

			result.Int(total);

			if (this.love.Count > 0)
			{
				MessengerFriend friend = this.love[rand.Next(this.love.Count)];
				result.Int(1);
				result.Int(love.Count);
				result.Int(friend.Id);
				result.String(friend.Username);
				result.String(friend.Look);
			}
			if (this.happy.Count > 0)
			{
				MessengerFriend friend = this.happy[rand.Next(this.happy.Count)];
				result.Int(2);
				result.Int(happy.Count);
				result.Int(friend.Id);
				result.String(friend.Username);
				result.String(friend.Look);
			}
			if (this.sad.Count > 0)
			{
				MessengerFriend friend = this.sad[rand.Next(this.sad.Count)];
				result.Int(3);
				result.Int(love.Count);
				result.Int(friend.Id);
				result.String(friend.Username);
				result.String(friend.Look);
			}
			return result;
		}
	}
}
