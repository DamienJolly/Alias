using System.Collections.Generic;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Events
{
	public class InviteFriendsEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (session.Habbo().Muted)
			{
				return;
			}

			int amount = message.Integer();
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
				friends.Add(message.Integer());
			}

			session.Habbo().Messenger().RoomInvitation(friends, message.String());
		}
	}
}
