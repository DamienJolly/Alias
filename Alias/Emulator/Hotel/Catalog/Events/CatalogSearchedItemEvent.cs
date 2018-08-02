using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Catalog.Events
{
	class CatalogSearchedItemEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int offerId = message.PopInt();

			CatalogPage page = Alias.Server.CatalogManager.GetCatalogPageByOffer(offerId);
			if (page != null)
			{
				CatalogItem item = page.GetCatalogItemByOffer(offerId);
				if (item != null)
				{
					session.Send(new CatalogSearchResultComposer(item));
				}
			}
		}
	}
}
