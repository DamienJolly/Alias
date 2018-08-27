using Alias.Emulator.Hotel.Users.Currency;
using Alias.Emulator.Hotel.Users.Events;
using Alias.Emulator.Hotel.Users.Handshake;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Users.Messenger;
using Alias.Emulator.Hotel.Users.Subscription;
using Alias.Emulator.Hotel.Users.Wardrobe;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Users
{
	public class UserEvents
	{
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestUserProfileMessageEvent, new RequestUserProfileEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestMeMenuSettingsMessageEvent, new RequestMeMenuSettingsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.UsernameMessageEvent, new UsernameEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.SaveUserVolumesMessageEvent, new SaveUserVolumesEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.SavePreferOldChatMessageEvent, new SavePreferOldChatEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.SaveIgnoreRoomInvitesMessageEvent, new SaveIgnoreRoomInvitesEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.SaveBlockCameraFollowMessageEvent, new SaveBlockCameraFollowEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.UserWearBadgeMessageEvent, new UserWearBadgeEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestWearingBadgesMessageEvent, new RequestWearingBadgesEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestProfileFriendsMessageEvent, new RequestProfileFriendsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.UserActivityMessageEvent, new UserActivityEvent());

			WardrobeEvents.Register();
			MessengerEvents.Register();
			CurrencyEvents.Register();
			InventoryEvents.Register();
			HandshakeEvents.Register();
			SubscriptionEvents.Register();
		}
	}
}
