using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Misc.Events
{
	class VersionCheckEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			// not used
			return;
		}
	}
}
