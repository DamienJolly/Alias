using Alias.Emulator.Hotel.Rooms.Events;
using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Hotel.Rooms.Rights;
using Alias.Emulator.Hotel.Rooms.Trading;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;

namespace Alias.Emulator.Hotel.Rooms
{
	public class RoomEvents
	{
		public static void Register()
		{
			MessageHandler.Register(Incoming.RequestHeightmapMessageEvent, new RequestHeightmapEvent());
			MessageHandler.Register(Incoming.RequestRoomDataMessageEvent, new RequestRoomDataEvent());
			MessageHandler.Register(Incoming.RequestRoomLoadMessageEvent, new RequestRoomLoadEvent());
			MessageHandler.Register(Incoming.RequestRoomHeightmapMessageEvent, new RequestRoomHeightmapEvent());

			RoomTradingEvents.Register();
			RoomRightsEvents.Register();
			RoomUserEvents.Register();
			RoomItemEvents.Register();
		}
	}
}
