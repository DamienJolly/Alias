using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Network.Packets
{
	interface IPacketEvent
	{
		void Handle(Session session, ClientPacket message);
	}
}
