using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Handshake.Events
{
	public class VersionCheckEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			string gordanPath = message.String();
			string externalVariables = message.String();
		}
	}
}
