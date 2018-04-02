using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutRecycler : ICatalogLayout
	{
		public void Serialize(ServerPacket message, CatalogPage page)
		{
			message.WriteString("recycler");
			message.WriteInteger(2);
			message.WriteString(page.HeaderImage);
			message.WriteString(page.TeaserImage);
			message.WriteInteger(1);
			message.WriteString(page.TextOne);
			message.WriteInteger(-1);
			message.WriteBoolean(false);
		}
	}
}