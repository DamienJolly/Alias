using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Events
{
	public class DeclineFriendRequestEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (message.Boolean())
			{
				session.Habbo.Messenger.DeclineAll();
			}
			else
			{
				message.Integer();
				session.Habbo.Messenger.Decline(message.Integer());
			}
		}
	}
}
