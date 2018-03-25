using Alias.Emulator.Hotel.Users.Currency.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Users.Currency
{
	public class CurrencyEvents
	{
		public static void Register()
		{
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestUserCreditsMessageEvent, new RequestUserCreditsEvent());
		}
	}
}
