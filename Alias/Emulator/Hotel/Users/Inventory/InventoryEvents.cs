using Alias.Emulator.Hotel.Users.Inventory.Events;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;

namespace Alias.Emulator.Hotel.Users.Inventory
{
	public class InventoryEvents
	{
		public static void Register()
		{
			MessageHandler.Register(Incoming.RequestInventoryItemsMessageEvent, new RequestInventoryItemsEvent());
			MessageHandler.Register(Incoming.RequestInventoryBadgesMessageEvent, new RequestInventoryBadgesEvent());
		}
	}
}
