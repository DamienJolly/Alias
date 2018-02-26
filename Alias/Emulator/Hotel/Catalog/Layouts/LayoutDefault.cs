using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutDefault : ICatalogLayout
	{
		public void Serialize(ServerMessage message, CatalogPage page)
		{
			message.String("default_3x3");
			message.Int(3);
			message.String(page.HeaderImage);
			message.String(page.TeaserImage);
			message.String(page.SpecialImage);
			message.Int(3);
			message.String(page.TextOne);
			message.String(page.TextDetails);
			message.String(page.TextTeaser);
		}
	}
}