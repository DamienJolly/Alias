using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Layouts
{
	public interface ICatalogLayout
	{
		void Serialize(ServerMessage message, CatalogPage page);
	}
}