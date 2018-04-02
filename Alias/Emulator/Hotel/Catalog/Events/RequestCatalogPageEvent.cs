using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Catalog.Events
{
	public class RequestCatalogPageEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int catalogPageId = message.PopInt();
			int unknown = message.PopInt();
			string mode = message.PopString();

			CatalogPage page = Alias.Server.CatalogManager.GetCatalogPage(catalogPageId);
			if (catalogPageId > 0 && page != null)
			{
				if (page.Rank <= session.Habbo.Rank && page.Enabled)
				{
					session.Send(new CatalogPageComposer(page, session.Habbo, mode));
				}
			}
		}
	}
}
