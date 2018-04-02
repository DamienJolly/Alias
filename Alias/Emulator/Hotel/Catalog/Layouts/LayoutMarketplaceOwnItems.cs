using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutMarketplaceOwnItems : ICatalogLayout
	{
		public void Serialize(ServerPacket message, CatalogPage page)
		{
			message.WriteString("marketplace_own_items");
			message.WriteInteger(0);
			message.WriteInteger(0);
		}
	}
}