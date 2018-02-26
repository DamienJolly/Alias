using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutClubBuy : ICatalogLayout
	{
		public void Serialize(ServerMessage message, CatalogPage page)
		{
			message.String("club_buy");
			message.Int(2);
			message.String(page.HeaderImage);
			message.String(page.TeaserImage);
			message.Int(0);
		}
	}
}