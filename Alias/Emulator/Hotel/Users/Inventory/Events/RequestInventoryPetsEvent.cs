using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Inventory.Events
{
	class RequestInventoryPetsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new InventoryPetsComposer(session.Habbo.Inventory.Pets));
		}
	}
}
