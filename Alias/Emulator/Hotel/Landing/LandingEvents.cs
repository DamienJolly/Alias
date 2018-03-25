using Alias.Emulator.Hotel.Landing.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Landing
{
	public class LandingEvents
    {
		public static void Register()
		{
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestNewsListMessageEvent, new RequestNewsListEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.HotelViewDataMessageEvent, new HotelViewDataEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.HotelViewMessageEvent, new HotelViewEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.HotelViewRequestBonusRareMessageEvent, new HotelViewRequestBonusRareEvent());
		}
	}
}
