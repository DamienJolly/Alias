using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Network.Messages
{
	public interface MessageEvent
	{
		void Handle(Session session, ClientMessage message);
	}
}
