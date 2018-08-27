using Alias.Emulator.Hotel.Misc.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Misc
{
    class MiscEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.LatencyTestMessageEvent, new LatencyTestEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestTargetOfferMessageEvent, new RequestTargetOfferEvent());

			// Unknown
			Alias.Server.SocketServer.PacketManager.Register(Incoming.UnknownMessageEvent1, new UnknownEvent1());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.UnknownMessageEvent2, new UnknownEvent2());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.UnknownMessageEvent3, new UnknownEvent3());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.UnknownMessageEvent4, new UnknownEvent4());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.UnknownMessageEvent5, new UnknownEvent5());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.UnknownMessageEvent6, new UnknownEvent6());
		}
	}
}
