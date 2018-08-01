using Alias.Emulator.Hotel.Rooms.Promotions.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Rooms.Promotions
{
	public class RoomPromotionEvents
	{
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestPromotionRoomsMessageEvent, new RequestPromotionRoomsEvent());
		}
	}
}
