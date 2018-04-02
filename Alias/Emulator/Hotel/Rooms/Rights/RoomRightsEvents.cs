using Alias.Emulator.Hotel.Rooms.Rights.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Rooms.Rights
{
    public class RoomRightsEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestRoomRightsMessageEvent, new RequestRoomRightsEvent());
		}
    }
}
