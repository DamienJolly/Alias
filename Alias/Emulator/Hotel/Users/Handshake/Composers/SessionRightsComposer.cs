using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Handshake.Composers
{
	public class SessionRightsComposer : IMessageComposer
	{
		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.SessionRightsMessageComposer);
			result.Boolean(true);
			result.Boolean(false);
			result.Boolean(true);
			return result;
		}
	}
}
