using Alias.Emulator.Hotel.Landing.Events;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;

namespace Alias.Emulator.Hotel.Landing
{
	public class LandingEvents
    {
		public static void Register()
		{
			MessageHandler.Register(Incoming.RequestNewsListMessageEvent, new RequestNewsListEvent());
			MessageHandler.Register(Incoming.HotelViewDataMessageEvent, new HotelViewDataEvent());
			MessageHandler.Register(Incoming.HotelViewMessageEvent, new HotelViewEvent());
			MessageHandler.Register(Incoming.HotelViewRequestBonusRareMessageEvent, new HotelViewRequestBonusRareEvent());
		}
	}
}
