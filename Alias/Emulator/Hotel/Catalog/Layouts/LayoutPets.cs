using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutPets : ICatalogLayout
	{
		public void Serialize(ServerPacket message, CatalogPage page)
		{
			message.WriteString("pets");
			message.WriteInteger(2);
			message.WriteString(page.HeaderImage);
			message.WriteString(page.TeaserImage);
			message.WriteInteger(4);
			message.WriteString(page.TextOne);
			message.WriteString(page.TextTwo);
			message.WriteString(page.TextDetails);
			message.WriteString(page.TextTeaser);
		}
	}
}