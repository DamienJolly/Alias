using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	public class SavePreferOldChatEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			bool oldChat = message.Boolean();

			session.Habbo().Settings.OldChat = oldChat;
		}
	}
}
