using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Catalog.Events
{
	public class CatalogBuyItemEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int pageId = message.PopInt();
			int itemId = message.PopInt();
			string extraData = message.PopString();
			int count = message.PopInt();

			CatalogPage page = Alias.Server.CatalogManager.GetCatalogPage(pageId);
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

			Alias.Server.CatalogManager.PurchaseItem(page, item, session.Habbo, count, extraData);
		}
	}
}
