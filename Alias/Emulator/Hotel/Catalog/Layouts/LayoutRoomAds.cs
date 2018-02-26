using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutRoomAds : ICatalogLayout
	{
		public void Serialize(ServerMessage message, CatalogPage page)
		{
			message.String("roomads");
			message.Int(2);
			message.String(page.HeaderImage);
			message.String(page.TeaserImage);
			message.Int(2);
			message.String(page.TextOne);
			message.String(page.TextTwo);
		}
	}
}