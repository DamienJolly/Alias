using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Misc.Events
{
    class RequestTargetOfferEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			// todo:
		}
	}
}
