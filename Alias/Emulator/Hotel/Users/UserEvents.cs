using Alias.Emulator.Hotel.Users.Currency;
using Alias.Emulator.Hotel.Users.Events;
using Alias.Emulator.Hotel.Users.Handshake;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Users.Messenger;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Users
{
	public class UserEvents
	{
		public static void Register()
		{
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestUserProfileMessageEvent, new RequestUserProfileEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestMeMenuSettingsMessageEvent, new RequestMeMenuSettingsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.UsernameMessageEvent, new UsernameEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.SaveUserVolumesMessageEvent, new SaveUserVolumesEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.SavePreferOldChatMessageEvent, new SavePreferOldChatEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.SaveIgnoreRoomInvitesMessageEvent, new SaveIgnoreRoomInvitesEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.SaveBlockCameraFollowMessageEvent, new SaveBlockCameraFollowEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.UserWearBadgeMessageEvent, new UserWearBadgeEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestWearingBadgesMessageEvent, new RequestWearingBadgesEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.UserSaveLookMessageEvent, new UserSaveLookEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestProfileFriendsMessageEvent, new RequestProfileFriendsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.UserActivityMessageEvent, new UserActivityEvent());

			MessengerEvents.Register();
			CurrencyEvents.Register();
			InventoryEvents.Register();
			HandshakeEvents.Register();
		}
	}
}
