using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Inventory.Events
{
	class RequestInventoryBotsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new InventoryBotsComposer(session.Habbo.Inventory.GetBots));
		}
	}
}
