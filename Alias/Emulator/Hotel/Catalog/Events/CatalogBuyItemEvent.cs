using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Catalog.Events
{
	public class CatalogBuyItemEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			int pageId = message.Integer();
			int itemId = message.Integer();
			string extraData = message.String();
			int count = message.Integer();

			CatalogPage page = Alias.GetServer().GetCatalogManager().GetCatalogPage(pageId);
			if (page == null)
			{
				session.Send(new AlertPurchaseFailedComposer(AlertPurchaseFailedComposer.SERVER_ERROR));
				return;
			}

			if (page.Rank > session.Habbo.Rank)
			{
				session.Send(new AlertPurchaseUnavailableComposer(AlertPurchaseUnavailableComposer.ILLEGAL));
				return;
			}

			CatalogItem item = page.GetCatalogItem(itemId);

			Alias.GetServer().GetCatalogManager().PurchaseItem(page, item, session.Habbo, count, extraData);
		}
	}
}
