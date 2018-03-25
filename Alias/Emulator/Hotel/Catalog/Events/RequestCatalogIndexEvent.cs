using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Catalog.Events
{
	public class RequestCatalogIndexEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			// Not used..
		}
	}
}
