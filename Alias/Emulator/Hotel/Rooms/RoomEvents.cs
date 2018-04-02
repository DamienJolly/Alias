using Alias.Emulator.Hotel.Rooms.Events;
using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Hotel.Rooms.Rights;
using Alias.Emulator.Hotel.Rooms.Trading;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Rooms
{
	public class RoomEvents
	{
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestHeightmapMessageEvent, new RequestHeightmapEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestRoomDataMessageEvent, new RequestRoomDataEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestRoomLoadMessageEvent, new RequestRoomLoadEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestRoomHeightmapMessageEvent, new RequestRoomHeightmapEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestRoomSettingsMessageEvent, new RequestRoomSettingsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomSettingsSaveMessageEvent, new RoomSettingsSaveEvent());

			RoomTradingEvents.Register();
			RoomRightsEvents.Register();
			RoomUserEvents.Register();
			RoomItemEvents.Register();
		}
	}
}
