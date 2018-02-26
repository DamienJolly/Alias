using Alias.Emulator.Hotel.Users.Currency.Events;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;

namespace Alias.Emulator.Hotel.Users.Currency
{
	public class CurrencyEvents
	{
		public static void Register()
		{
			MessageHandler.Register(Incoming.RequestUserCreditsMessageEvent, new RequestUserCreditsEvent());
		}
	}
}
