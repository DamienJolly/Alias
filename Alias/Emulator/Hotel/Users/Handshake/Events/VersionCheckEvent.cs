using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Handshake.Events
{
	public class VersionCheckEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			// not used
			return;
		}
	}
}
