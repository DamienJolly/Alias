using Alias.Emulator.Hotel.Catalog.Recycler.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Catalog.Recycler
{
    class RecyclerEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ReloadRecyclerMessageEvent, new ReloadRecyclerEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestRecyclerLogicMessageEvent, new RequestRecyclerLogicEvent());
		}
	}
}
