using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutInfoDuckets : ICatalogLayout
	{
		public void Serialize(ServerMessage message, CatalogPage page)
		{
			message.String("info_duckets");
			message.Int(1);
			message.String(page.HeaderImage);
			message.Int(1);
			message.String(page.TextOne);
			message.Int(0);
		}
	}
}