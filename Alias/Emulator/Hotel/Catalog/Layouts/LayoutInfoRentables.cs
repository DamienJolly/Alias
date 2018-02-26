using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutInfoRentables : ICatalogLayout
	{
		public void Serialize(ServerMessage message, CatalogPage page)
		{
			//TODO
			string[] data = { };
			message.String("info_rentables");
			message.Int(1);
			message.String(page.HeaderImage);
			message.Int(data.Length);
			foreach (string str in data)
				message.String(str);
			message.Int(0);
		}
	}
}