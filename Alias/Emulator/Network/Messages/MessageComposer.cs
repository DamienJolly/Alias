using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Network.Messages
{
	public interface MessageComposer
	{
		ServerMessage Compose();
	}
}
