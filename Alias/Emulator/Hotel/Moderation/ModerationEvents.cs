using Alias.Emulator.Hotel.Moderation.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Moderation
{
    public class ModerationEvents
    {
		public static void Register()
		{
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationAlertMessageEvent, new ModerationAlertEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationRequestUserInfoMessageEvent, new ModerationRequestUserInfoEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationRequestRoomInfoMessageEvent, new ModerationRequestRoomInfoEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationRequestRoomVisitsMessageEvent, new ModerationRequestRoomVisitsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationRequestRoomChatlogMessageEvent, new ModerationRequestRoomChatlogEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationChangeRoomSettingsMessageEvent, new ModerationChangeRoomSettingsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationRequestUserChatlogMessageEvent, new ModerationRequestUserChatlogEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationKickMessageEvent, new ModerationKickEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationPickTicketMessageEvent, new ModerationPickTicketEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationReleaseTicketMessageEvent, new ModerationReleaseTicketEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationCloseTicketMessageEvent, new ModerationCloseTicketEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationRoomAlertMessageEvent, new ModerationRoomAlertEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationRequestIssueChatlogMessageEvent, new ModerationRequestIssueChatlogEvent());

			//sanction shit
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationSanctionAlertMessageEvent, new ModerationSanctionAlertEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationSanctionMuteMessageEvent, new ModerationSanctionMuteEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationSanctionBanMessageEvent, new ModerationSanctionBanEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ModerationSanctionTradeLockMessageEvent, new ModerationSanctionTradeLockEvent());
		}
	}
}
