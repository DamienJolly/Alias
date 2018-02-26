using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutInfoPets : ICatalogLayout
	{
		public void Serialize(ServerMessage message, CatalogPage page)
		{
			message.String("info_pets");
			message.Int(2);
			message.String(page.HeaderImage);
			message.String(page.TeaserImage);
			message.Int(3);
			message.String(page.TextOne);
			message.String("");
			message.String(page.TextTeaser);
			message.Int(0);
		}
	}
}