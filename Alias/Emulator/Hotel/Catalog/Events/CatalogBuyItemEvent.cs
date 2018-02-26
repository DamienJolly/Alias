using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Catalog.Events
{
	public class CatalogBuyItemEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			int pageId = message.Integer();
			int itemId = message.Integer();
			string extraData = message.String();
			int count = message.Integer();

			CatalogPage page = CatalogManager.GetCatalogPage(pageId);
			if (page == null)
			{
				session.Send(new AlertPurchaseFailedComposer(AlertPurchaseFailedComposer.SERVER_ERROR));
				return;
			}

			if (page.Rank > session.Habbo().Rank)
			{
				session.Send(new AlertPurchaseUnavailableComposer(AlertPurchaseUnavailableComposer.ILLEGAL));
				return;
			}

			CatalogItem item = page.GetCatalogItem(itemId);

			CatalogManager.PurchaseItem(page, item, session.Habbo(), count, extraData);
		}
	}
}
