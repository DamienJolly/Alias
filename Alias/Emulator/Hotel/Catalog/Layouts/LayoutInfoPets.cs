using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutInfoPets : ICatalogLayout
	{
		public void Serialize(ServerPacket message, CatalogPage page)
		{
			message.WriteString("info_pets");
			message.WriteInteger(2);
			message.WriteString(page.HeaderImage);
			message.WriteString(page.TeaserImage);
			message.WriteInteger(3);
			message.WriteString(page.TextOne);
			message.WriteString("");
			message.WriteString(page.TextTeaser);
			message.WriteInteger(0);
		}
	}
}