using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Events
{
	public class InviteFriendsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (session.Habbo.Muted)
			{
				return;
			}

			int amount = message.PopInt();
			if (amount > 100)
			{
				amount = 100;
			}
			else if (amount < 0)
			{
				amount = 0;
			}

			List<int> friends = new List<int>();
			for (int i = 0; i < amount; i++)
			{
				friends.Add(message.PopInt());
			}

			session.Habbo.Messenger.RoomInvitation(friends, message.PopString());
		}
	}
}
