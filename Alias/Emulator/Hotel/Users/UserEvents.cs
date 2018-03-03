using Alias.Emulator.Hotel.Users.Currency;
using Alias.Emulator.Hotel.Users.Events;
using Alias.Emulator.Hotel.Users.Handshake;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Users.Messenger;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;

namespace Alias.Emulator.Hotel.Users
{
	public class UserEvents
	{
		public static void Register()
		{
			MessageHandler.Register(Incoming.RequestUserProfileMessageEvent, new RequestUserProfileEvent());
			MessageHandler.Register(Incoming.RequestMeMenuSettingsMessageEvent, new RequestMeMenuSettingsEvent());
			MessageHandler.Register(Incoming.UsernameMessageEvent, new UsernameEvent());
			MessageHandler.Register(Incoming.SaveUserVolumesMessageEvent, new SaveUserVolumesEvent());
			MessageHandler.Register(Incoming.SavePreferOldChatMessageEvent, new SavePreferOldChatEvent());
			MessageHandler.Register(Incoming.SaveIgnoreRoomInvitesMessageEvent, new SaveIgnoreRoomInvitesEvent());
			MessageHandler.Register(Incoming.SaveBlockCameraFollowMessageEvent, new SaveBlockCameraFollowEvent());
			MessageHandler.Register(Incoming.UserWearBadgeMessageEvent, new UserWearBadgeEvent());
			MessageHandler.Register(Incoming.RequestWearingBadgesMessageEvent, new RequestWearingBadgesEvent());
			MessageHandler.Register(Incoming.UserSaveLookMessageEvent, new UserSaveLookEvent());
			MessageHandler.Register(Incoming.RequestProfileFriendsMessageEvent, new RequestProfileFriendsEvent());
			MessageHandler.Register(Incoming.UserActivityMessageEvent, new UserActivityEvent());

			MessengerEvents.Register();
			CurrencyEvents.Register();
			InventoryEvents.Register();
			HandshakeEvents.Register();
		}
	}
}
