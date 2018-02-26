using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutSingleBundle : ICatalogLayout
	{
		public void Serialize(ServerMessage message, CatalogPage page)
		{
			message.String("single_bundle");
			message.Int(3);
			message.String(page.HeaderImage);
			message.String(page.TeaserImage);
			message.String("");
			message.Int(4);
			message.String(page.TextOne);
			message.String(page.TextDetails);
			message.String(page.TextTeaser);
			message.String(page.TextTwo);
		}
	}
}