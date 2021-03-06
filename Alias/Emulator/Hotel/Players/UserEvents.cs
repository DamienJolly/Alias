using Alias.Emulator.Hotel.Players.Currency;
using Alias.Emulator.Hotel.Players.Events;
using Alias.Emulator.Hotel.Players.Inventory;
using Alias.Emulator.Hotel.Players.Messenger;
using Alias.Emulator.Hotel.Players.Wardrobe;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Players
{
	public class UserEvents
	{
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.MachineIDMessageEvent, new MachineIDEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestUserDataMessageEvent, new RequestUserDataEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.SecureLoginMessageEvent, new SecureLoginEvent());
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
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestUserClubMessageEvent, new RequestUserClubEvent());

			WardrobeEvents.Register();
			MessengerEvents.Register();
			CurrencyEvents.Register();
			InventoryEvents.Register();
		}
	}
}
