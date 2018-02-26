using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutMarketplaceOwnItems : ICatalogLayout
	{
		public void Serialize(ServerMessage message, CatalogPage page)
		{
			message.String("marketplace_own_items");
			message.Int(0);
			message.Int(0);
		}
	}
}