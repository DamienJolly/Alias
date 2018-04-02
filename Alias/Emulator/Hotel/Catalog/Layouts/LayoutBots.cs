using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutBots : ICatalogLayout
	{
		public void Serialize(ServerPacket message, CatalogPage page)
		{
			message.WriteString("bots");
			message.WriteInteger(2);
			message.WriteString(page.HeaderImage);
			message.WriteString(page.TeaserImage);
			message.WriteInteger(3);
			message.WriteString(page.TextOne);
			message.WriteString(page.TextDetails);
			message.WriteString(page.TextTwo);
		}
	}
}