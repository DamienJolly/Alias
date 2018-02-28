using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Network.Messages
{
	public interface IMessageComposer
	{
		ServerMessage Compose();
	}
}
