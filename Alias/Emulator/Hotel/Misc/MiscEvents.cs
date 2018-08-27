using Alias.Emulator.Hotel.Misc.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Misc
{
    class MiscEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.UnknownMessageEvent1, new UnknownEvent1());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.LatencyTestMessageEvent, new LatencyTestEvent());
		}
	}
}
