using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Catalog.Events
{
	public class RequestCatalogModeEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			string MODE = message.String();
			if (MODE.Equals("normal"))
			{
				session.Send(new CatalogModeComposer(0));
				session.Send(new CatalogPagesListComposer(session.Habbo, MODE));
			}
			else
			{
				session.Send(new CatalogModeComposer(1));
				session.Send(new CatalogPagesListComposer(session.Habbo, MODE));
			}
		}
	}
}
