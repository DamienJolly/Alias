using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutClubGift : ICatalogLayout
	{
		public void Serialize(ServerMessage message, CatalogPage page)
		{
			message.String("club_gifts");
			message.Int(1);
			message.String(page.HeaderImage);
			message.Int(1);
			message.String(page.TextOne);
		}
	}
}