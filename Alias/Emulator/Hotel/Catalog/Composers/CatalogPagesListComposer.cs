using System.Collections.Generic;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class CatalogPagesListComposer : MessageComposer
	{
		Habbo Habbo;
		List<CatalogPage> Pages;
		string MODE;

		public CatalogPagesListComposer(Habbo habbo, string MODE)
		{
			Habbo = habbo;
			this.Pages = CatalogManager.GetCatalogPages(-1, habbo);
			this.MODE = MODE;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.CatalogPagesListMessageComposer);
			message.Boolean(true);
			message.Int(0);
			message.Int(-1);
			message.String("root");
			message.String("");
			message.Int(0);

			message.Int(Pages.Count);
			Pages.ForEach(page =>
			{
				Append(message, page);
			});

			message.Boolean(false);
			message.String(this.MODE);
			return message;
		}

		private void Append(ServerMessage message, CatalogPage catalogPage)
		{
			List<CatalogPage> Pages = CatalogManager.GetCatalogPages(catalogPage.Id, Habbo);

			message.Boolean(catalogPage.Visible);
			message.Int(catalogPage.Icon);
			message.Int(catalogPage.Enabled ? catalogPage.Id : -1);
			message.String(catalogPage.Name);
			message.String(catalogPage.Caption + " (" + catalogPage.Id + ")");

			message.Int(catalogPage.OfferIds.Count);
			catalogPage.OfferIds.ForEach(id =>
			{
				message.Int(id);
			});

			message.Int(Pages.Count);
			Pages.ForEach(page =>
			{
				Append(message, page);
			});
		}
	}
}
