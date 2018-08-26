using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Catalog.Events
{
	class RequestGiftConfigurationEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			//todo: gift configuration
			session.Send(new GiftConfigurationComposer());
		}
	}
}
