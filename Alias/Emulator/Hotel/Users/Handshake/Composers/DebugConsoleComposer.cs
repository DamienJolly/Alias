using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Handshake.Composers
{
	public class DebugConsoleComposer : IMessageComposer
	{
		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.DebugConsoleMessageComposer);
			result.Boolean(true);
			return result;
		}
	}
}
