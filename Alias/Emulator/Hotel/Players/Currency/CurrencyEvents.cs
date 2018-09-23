using Alias.Emulator.Hotel.Players.Currency.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Players.Currency
{
	public class CurrencyEvents
	{
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestUserCreditsMessageEvent, new RequestUserCreditsEvent());
		}
	}
}
