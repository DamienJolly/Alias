using Alias.Emulator.Hotel.Users.Inventory.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Users.Inventory
{
	public class InventoryEvents
	{
		public static void Register()
		{
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestInventoryItemsMessageEvent, new RequestInventoryItemsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestInventoryBadgesMessageEvent, new RequestInventoryBadgesEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestInventoryBotsMessageEvent, new RequestInventoryBotsEvent());
		}
	}
}
