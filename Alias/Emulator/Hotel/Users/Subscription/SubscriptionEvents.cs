using Alias.Emulator.Hotel.Users.Subscription.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Users.Subscription
{
	class SubscriptionEvents
	{
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestUserClubMessageEvent, new RequestUserClubEvent());
		}
	}
}
