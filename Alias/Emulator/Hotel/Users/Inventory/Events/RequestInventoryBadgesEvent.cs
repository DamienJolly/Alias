using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Inventory.Events
{
	public class RequestInventoryBadgesEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			session.Send(new InventoryBadgesComposer(session.Habbo));
		}
	}
}