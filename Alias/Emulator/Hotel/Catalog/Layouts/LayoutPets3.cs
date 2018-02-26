﻿using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutPets3 : ICatalogLayout
	{
		public void Serialize(ServerMessage message, CatalogPage page)
		{
			message.String("pets3");
			message.Int(2);
			message.String(page.HeaderImage);
			message.String(page.TeaserImage);
			message.Int(4);
			message.String(page.TextOne);
			message.String(page.TextTwo);
			message.String(page.TextDetails);
			message.String(page.TextTeaser);
		}
	}
}