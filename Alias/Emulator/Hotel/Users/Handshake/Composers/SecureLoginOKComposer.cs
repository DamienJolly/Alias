using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Handshake.Composers
{
	public class SecureLoginOKComposer : IMessageComposer
	{
		public ServerMessage Compose()
		{
			return new ServerMessage(Outgoing.SecureLoginOKMessageComposer);
		}
	}
}
