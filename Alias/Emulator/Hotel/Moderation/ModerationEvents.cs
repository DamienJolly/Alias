using Alias.Emulator.Hotel.Moderation.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Moderation
{
    public class ModerationEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationAlertMessageEvent, new ModerationAlertEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationRequestUserInfoMessageEvent, new ModerationRequestUserInfoEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationRequestRoomInfoMessageEvent, new ModerationRequestRoomInfoEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationRequestRoomVisitsMessageEvent, new ModerationRequestRoomVisitsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationRequestRoomChatlogMessageEvent, new ModerationRequestRoomChatlogEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationChangeRoomSettingsMessageEvent, new ModerationChangeRoomSettingsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationRequestUserChatlogMessageEvent, new ModerationRequestUserChatlogEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationKickMessageEvent, new ModerationKickEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationPickTicketMessageEvent, new ModerationPickTicketEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationReleaseTicketMessageEvent, new ModerationReleaseTicketEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationCloseTicketMessageEvent, new ModerationCloseTicketEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationRoomAlertMessageEvent, new ModerationRoomAlertEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationRequestIssueChatlogMessageEvent, new ModerationRequestIssueChatlogEvent());

			//sanction shit
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationSanctionAlertMessageEvent, new ModerationSanctionAlertEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationSanctionMuteMessageEvent, new ModerationSanctionMuteEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationSanctionBanMessageEvent, new ModerationSanctionBanEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ModerationSanctionTradeLockMessageEvent, new ModerationSanctionTradeLockEvent());
		}
	}
}
