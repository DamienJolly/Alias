﻿using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutTrophies : ICatalogLayout
	{
		public void Serialize(ServerPacket message, CatalogPage page)
		{
			message.WriteString("trophies");
			message.WriteInteger(3);
			message.WriteString(page.HeaderImage);
			message.WriteString(page.TeaserImage);
			message.WriteString(page.SpecialImage);
			message.WriteInteger(3);
			message.WriteString(page.TextOne);
			message.WriteString(page.TextDetails);
			message.WriteString(page.TextTeaser);
		}
	}
}