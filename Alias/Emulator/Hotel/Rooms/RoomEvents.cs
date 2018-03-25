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
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestHeightmapMessageEvent, new RequestHeightmapEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestRoomDataMessageEvent, new RequestRoomDataEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestRoomLoadMessageEvent, new RequestRoomLoadEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestRoomHeightmapMessageEvent, new RequestRoomHeightmapEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestRoomSettingsMessageEvent, new RequestRoomSettingsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RoomSettingsSaveMessageEvent, new RoomSettingsSaveEvent());

			RoomTradingEvents.Register();
			RoomRightsEvents.Register();
			RoomUserEvents.Register();
			RoomItemEvents.Register();
		}
	}
}
