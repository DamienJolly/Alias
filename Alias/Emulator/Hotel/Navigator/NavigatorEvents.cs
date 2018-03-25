using Alias.Emulator.Hotel.Navigator.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Navigator
{
	public class NavigatorEvents
	{
		public static void Register()
		{
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestNavigatorSettingsMessageEvent, new RequestNavigatorSettingsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.SaveWindowSettingsMessageEvent, new SaveWindowSettingsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.SearchRoomsMessageEvent, new SearchRoomsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestRoomCategoriesMessageEvent, new RequestRoomCategoriesEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestNavigatorDataMessageEvent, new RequestNavigatorDataEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestPromotedRoomsMessageEvent, new RequestPromotedRoomsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.AddSavedSearchMessageEvent, new AddSavedSearchEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RemoveSavedSearchMessageEvent, new RemoveSavedSearchEvent());
		}
	}
}
