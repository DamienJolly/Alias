using Alias.Emulator.Hotel.Landing.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Landing
{
	public class LandingEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestNewsListMessageEvent, new RequestNewsListEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.HotelViewDataMessageEvent, new HotelViewDataEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.HotelViewMessageEvent, new HotelViewEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.HotelViewRequestBonusRareMessageEvent, new HotelViewRequestBonusRareEvent());
		}
	}
}
