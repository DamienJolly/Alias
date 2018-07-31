using Alias.Emulator.Hotel.Navigator.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Navigator
{
	public class NavigatorEvents
	{
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestNavigatorSettingsMessageEvent, new RequestNavigatorSettingsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.SaveWindowSettingsMessageEvent, new SaveWindowSettingsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.SearchRoomsMessageEvent, new SearchRoomsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestRoomCategoriesMessageEvent, new RequestRoomCategoriesEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestNavigatorDataMessageEvent, new RequestNavigatorDataEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestPromotedRoomsMessageEvent, new RequestPromotedRoomsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.AddSavedSearchMessageEvent, new AddSavedSearchEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RemoveSavedSearchMessageEvent, new RemoveSavedSearchEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestCreateRoomMessageEvent, new RequestCreateRoomEvent());
		}
	}
}
