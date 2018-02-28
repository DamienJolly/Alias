using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Events
{
	public class FriendPrivateMessageEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (session.Habbo.Muted)
			{
				return;
			}

			session.Habbo.Messenger.Message(message.Integer(), message.String());
		}
	}
}
