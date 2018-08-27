using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Misc.Events
{
    class LatencyTestEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			//
		}
	}
}
