using Alias.Emulator.Hotel.Users.Currency;
using Alias.Emulator.Hotel.Users.Handshake;
using Alias.Emulator.Hotel.Users.Inventory;

namespace Alias.Emulator.Hotel.Users
{
	public class UserEvents
	{
		public static void Register()
		{
			CurrencyEvents.Register();
			InventoryEvents.Register();
			HandshakeEvents.Register();
		}
	}
}
