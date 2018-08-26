using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Catalog.Marketplace
{
    class MarketplaceEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestMarketplaceConfigMessageEvent, new RequestMarketplaceConfigEvent());
		}
	}
}
