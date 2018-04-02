using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public class LayoutInfoRentables : ICatalogLayout
	{
		public void Serialize(ServerPacket message, CatalogPage page)
		{
			//TODO
			string[] data = { };
			message.WriteString("info_rentables");
			message.WriteInteger(1);
			message.WriteString(page.HeaderImage);
			message.WriteInteger(data.Length);
			foreach (string str in data)
				message.WriteString(str);
			message.WriteInteger(0);
		}
	}
}