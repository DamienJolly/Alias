using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutInfoLoyalty : ICatalogLayout
	{
		public void Serialize(ServerPacket message, CatalogPage page)
		{
			message.WriteString("info_loyalty");
			message.WriteInteger(1);
			message.WriteString(page.HeaderImage);
			message.WriteInteger(1);
			message.WriteString(page.TextOne);
			message.WriteInteger(0);
		}
	}
}