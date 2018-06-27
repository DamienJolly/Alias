using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Network.Packets
{
	interface IPacketComposer
	{
		ServerPacket Compose();
	}
}
