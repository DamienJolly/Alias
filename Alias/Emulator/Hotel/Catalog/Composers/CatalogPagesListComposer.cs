using System.Collections.Generic;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class CatalogPagesListComposer : IPacketComposer
	{
		Habbo Habbo;
		List<CatalogPage> Pages;
		string MODE;

		public CatalogPagesListComposer(Habbo habbo, string MODE)
		{
			Habbo = habbo;
			this.Pages = Alias.Server.CatalogManager.GetCatalogPages(-1, habbo);
			this.MODE = MODE;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.CatalogPagesListMessageComposer);
			message.WriteBoolean(true);
			message.WriteInteger(0);
			message.WriteInteger(-1);
			message.WriteString("root");
			message.WriteString("");
			message.WriteInteger(0);

			message.WriteInteger(Pages.Count);
			Pages.ForEach(page =>
			{
				Append(message, page);
			});

			message.WriteBoolean(false);
			message.WriteString(this.MODE);
			return message;
		}

		private void Append(ServerPacket message, CatalogPage catalogPage)
		{
			List<CatalogPage> Pages = Alias.Server.CatalogManager.GetCatalogPages(catalogPage.Id, Habbo);

			message.WriteBoolean(catalogPage.Visible);
			message.WriteInteger(catalogPage.Icon);
			message.WriteInteger(catalogPage.Enabled ? catalogPage.Id : -1);
			message.WriteString(catalogPage.Name);
			message.WriteString(catalogPage.Caption + " (" + catalogPage.Id + ")");

			message.WriteInteger(catalogPage.OfferIds.Count);
			catalogPage.OfferIds.ForEach(id =>
			{
				message.WriteInteger(id);
			});

			message.WriteInteger(Pages.Count);
			Pages.ForEach(page =>
			{
				Append(message, page);
			});
		}
	}
}
