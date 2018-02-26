using Alias.Emulator.Hotel.Catalog.Events;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;

namespace Alias.Emulator.Hotel.Catalog
{
    public class CatalogEvents
    {
		public static void Register()
		{
			MessageHandler.Register(Incoming.RequestCatalogIndexMessageEvent, new RequestCatalogIndexEvent());
			MessageHandler.Register(Incoming.RequestCatalogModeMessageEvent, new RequestCatalogModeEvent());
			MessageHandler.Register(Incoming.RequestCatalogPageMessageEvent, new RequestCatalogPageEvent());
			MessageHandler.Register(Incoming.RequestDiscountMessageEvent, new RequestDiscountEvent());
			MessageHandler.Register(Incoming.CatalogBuyItemMessageEvent, new CatalogBuyItemEvent());
		}
	}
}
