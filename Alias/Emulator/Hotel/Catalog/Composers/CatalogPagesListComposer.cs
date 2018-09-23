using System.Collections.Generic;
using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	class CatalogPagesListComposer : IPacketComposer
	{
		private readonly Player _habbo;
		private readonly IList<CatalogPage> _pages;
		private readonly string MODE;

		public CatalogPagesListComposer(Player habbo, string MODE)
		{
			_habbo = habbo;
			_pages = Alias.Server.CatalogManager.GetCatalogPages(-1, habbo);
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

			message.WriteInteger(_pages.Count);
			foreach (CatalogPage page in _pages)
			{
				Append(message, page);
			}

			message.WriteBoolean(false);
			message.WriteString(this.MODE);
			return message;
		}

		private void Append(ServerPacket message, CatalogPage catalogPage)
		{
			IList<CatalogPage> pages = Alias.Server.CatalogManager.GetCatalogPages(catalogPage.Id, _habbo);

			message.WriteBoolean(catalogPage.Visible);
			message.WriteInteger(catalogPage.Icon);
			message.WriteInteger(catalogPage.Enabled ? catalogPage.Id : -1);
			message.WriteString(catalogPage.Name);
			message.WriteString(catalogPage.Caption + " (" + catalogPage.Id + ")");

			message.WriteInteger(catalogPage.OfferIds.Count);
			foreach (int offerId in catalogPage.OfferIds)
			{
				message.WriteInteger(offerId);
			}

			message.WriteInteger(pages.Count);
			foreach (CatalogPage page in pages)
			{
				Append(message, page);
			}
		}
	}
}
