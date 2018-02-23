using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Handshake.Events
{
	public class SecureLoginEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			Handshake.OnLogin(message.String(), session);
		}
	}
}
