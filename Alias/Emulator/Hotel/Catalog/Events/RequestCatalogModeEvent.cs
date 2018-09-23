using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Catalog.Events
{
	class RequestCatalogModeEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			string MODE = message.PopString();
			if (MODE.Equals("normal"))
			{
				session.Send(new CatalogModeComposer(0));
				session.Send(new CatalogPagesListComposer(session.Player, MODE));
			}
			else
			{
				session.Send(new CatalogModeComposer(1));
				session.Send(new CatalogPagesListComposer(session.Player, MODE));
			}
		}
	}
}
