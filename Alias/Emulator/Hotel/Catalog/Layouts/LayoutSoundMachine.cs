using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutSoundMachine : ICatalogLayout
	{
		public void Serialize(ServerPacket message, CatalogPage page)
		{
			message.WriteString("soundmachine");
			message.WriteInteger(2);
			message.WriteString(page.HeaderImage);
			message.WriteString(page.TeaserImage);
			message.WriteInteger(2);
			message.WriteString(page.TextOne);
			message.WriteString(page.TextDetails);
		}
	}
}