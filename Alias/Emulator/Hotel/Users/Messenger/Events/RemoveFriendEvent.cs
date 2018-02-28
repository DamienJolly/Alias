using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Events
{
	public class RemoveFriendEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			int amount = message.Integer();
			if (amount > 100)
			{
				amount = 100;
			}
			else if (amount < 0)
			{
				amount = 0;
			}

			for (int i = 0; i < amount; i++)
			{
				session.Habbo.Messenger.RemoveFriend(message.Integer());
			}
		}
	}
}
