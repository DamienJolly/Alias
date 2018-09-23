using Alias.Emulator.Hotel.Players.Inventory.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Inventory.Events
{
	class RequestInventoryBadgesEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new InventoryBadgesComposer(session.Player));
		}
	}
}
