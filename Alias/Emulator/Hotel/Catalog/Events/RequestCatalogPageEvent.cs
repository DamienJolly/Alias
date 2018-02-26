using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Catalog.Events
{
	public class RequestCatalogPageEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			int catalogPageId = message.Integer();
			int unknown = message.Integer();
			string mode = message.String();

			CatalogPage page = CatalogManager.GetCatalogPage(catalogPageId);
			if (catalogPageId > 0 && page != null)
			{
				if (page.Rank <= session.Habbo().Rank && page.Enabled)
				{
					session.Send(new CatalogPageComposer(page, session.Habbo(), mode));
				}
			}
		}
	}
}
