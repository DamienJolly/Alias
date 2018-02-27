using Alias.Emulator.Hotel.Rooms.Rights.Events;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;

namespace Alias.Emulator.Hotel.Rooms.Rights
{
    public class RoomRightsEvents
    {
		public static void Register()
		{
			MessageHandler.Register(Incoming.RequestRoomRightsMessageEvent, new RequestRoomRightsEvent());
		}
    }
}
