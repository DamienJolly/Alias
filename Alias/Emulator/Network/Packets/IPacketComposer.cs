using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Network.Packets
{
	public interface IPacketComposer
	{
		ServerMessage Compose();
	}
}
