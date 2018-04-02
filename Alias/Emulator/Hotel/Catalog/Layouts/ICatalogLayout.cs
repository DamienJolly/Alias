using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public interface ICatalogLayout
	{
		void Serialize(ServerPacket message, CatalogPage page);
	}
}