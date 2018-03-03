using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	public class UserActivityEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			string type = message.String();
			string value = message.String();

			//todo: do some shit here or w.e
		}
	}
}
