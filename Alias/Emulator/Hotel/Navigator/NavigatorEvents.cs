using Alias.Emulator.Hotel.Navigator.Events;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;

namespace Alias.Emulator.Hotel.Navigator
{
	public class NavigatorEvents
	{
		public static void Register()
		{
			MessageHandler.Register(Incoming.RequestNavigatorSettingsMessageEvent, new RequestNavigatorSettingsEvent());
			MessageHandler.Register(Incoming.SaveWindowSettingsMessageEvent, new SaveWindowSettingsEvent());
			MessageHandler.Register(Incoming.SearchRoomsMessageEvent, new SearchRoomsEvent());
			MessageHandler.Register(Incoming.RequestRoomCategoriesMessageEvent, new RequestRoomCategoriesEvent());
			MessageHandler.Register(Incoming.RequestNavigatorDataMessageEvent, new RequestNavigatorDataEvent());
			MessageHandler.Register(Incoming.RequestPromotedRoomsMessageEvent, new RequestPromotedRoomsEvent());
			MessageHandler.Register(Incoming.AddSavedSearchMessageEvent, new AddSavedSearchEvent());
			MessageHandler.Register(Incoming.RemoveSavedSearchMessageEvent, new RemoveSavedSearchEvent());
		}
	}
}
