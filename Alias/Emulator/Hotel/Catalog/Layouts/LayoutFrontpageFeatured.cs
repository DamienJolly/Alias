using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutFrontpageFeatured : ICatalogLayout
	{
		public void Serialize(ServerMessage message, CatalogPage page)
		{
			message.String("frontpage_featured");
			message.Int(2);
			message.String(page.HeaderImage);
			message.String(page.TeaserImage);
			message.Int(3);
			message.String(page.TextOne);
			message.String(page.TextTwo);
			message.String(page.TextTeaser);
		}
	}
}