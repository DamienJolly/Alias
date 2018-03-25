using Alias.Emulator.Hotel.Catalog.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Catalog
{
    public class CatalogEvents
    {
		public static void Register()
		{
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestCatalogIndexMessageEvent, new RequestCatalogIndexEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestCatalogModeMessageEvent, new RequestCatalogModeEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestCatalogPageMessageEvent, new RequestCatalogPageEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestDiscountMessageEvent, new RequestDiscountEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.CatalogBuyItemMessageEvent, new CatalogBuyItemEvent());
		}
	}
}
