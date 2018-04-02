using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutClubGift : ICatalogLayout
	{
		public void Serialize(ServerPacket message, CatalogPage page)
		{
			message.WriteString("club_gifts");
			message.WriteInteger(1);
			message.WriteString(page.HeaderImage);
			message.WriteInteger(1);
			message.WriteString(page.TextOne);
		}
	}
}