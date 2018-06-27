using Alias.Emulator.Hotel.Users.Inventory.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Users.Inventory
{
	public class InventoryEvents
	{
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestInventoryItemsMessageEvent, new RequestInventoryItemsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestInventoryBadgesMessageEvent, new RequestInventoryBadgesEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestInventoryBotsMessageEvent, new RequestInventoryBotsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestInventoryPetsMessageEvent, new RequestInventoryPetsEvent());
		}
	}
}
