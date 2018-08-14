using Alias.Emulator.Hotel.Catalog.Events;
using Alias.Emulator.Hotel.Catalog.Recycler;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Catalog
{
    public class CatalogEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestCatalogIndexMessageEvent, new RequestCatalogIndexEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestCatalogModeMessageEvent, new RequestCatalogModeEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestCatalogPageMessageEvent, new RequestCatalogPageEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestDiscountMessageEvent, new RequestDiscountEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.CatalogBuyItemMessageEvent, new CatalogBuyItemEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.CatalogSearchedItemMessageEvent, new CatalogSearchedItemEvent());

			RecyclerEvents.Register();
		}
	}
}
